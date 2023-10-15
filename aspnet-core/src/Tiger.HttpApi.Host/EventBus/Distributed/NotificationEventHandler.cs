using Tiger.Infrastructure.Notification.Core;
using Tiger.Infrastructure.Notification;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Json;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TextTemplating;
using System.Collections.Generic;
using System;
using System.Linq;
using Tiger.Module.Notifications.Enums;
using System.Globalization;
using Volo.Abp.Localization;
using Volo.Abp.Uow;
using Tiger.MultiTenancy;

namespace Tiger.EventBus.Distributed
{
    /// <summary>
    /// 订阅通知发布事件,统一发布消息
    /// </summary>
    /// <remarks>
    /// 作用在于SignalR客户端只会与一台服务器建立连接,
    /// 只有启用了SignlR服务端的才能真正将消息发布到客户端
    /// </remarks>
    public class NotificationEventHandler :
        IDistributedEventHandler<NotificationEto<NotificationData>>,
        IDistributedEventHandler<NotificationEto<NotificationTemplate>>,
        ITransientDependency
    {
        #region 构造函数
        /// <summary>
        /// Reference to <see cref="ILogger<DefaultNotificationDispatcher>"/>.
        /// </summary>
        public ILogger<NotificationEventHandler> Logger { get; set; }
        /// <summary>
        /// Reference to <see cref="AbpNotificationsPublishOptions"/>.
        /// </summary>
        protected AbpNotificationsPublishOptions Options { get; }
        /// <summary>
        /// Reference to <see cref="ICurrentTenant"/>.
        /// </summary>
        protected ICurrentTenant CurrentTenant { get; }
        /// <summary>
        /// Reference to <see cref="ITenantConfigurationCache"/>.
        /// </summary>
        protected ITenantConfigurationCache TenantConfigurationCache { get; }
        /// <summary>
        /// Reference to <see cref="IJsonSerializer"/>.
        /// </summary>
        protected IJsonSerializer JsonSerializer { get; }
        /// <summary>
        /// Reference to <see cref="IBackgroundJobManager"/>.
        /// </summary>
        protected IBackgroundJobManager BackgroundJobManager { get; }
        /// <summary>
        /// Reference to <see cref="ITemplateRenderer"/>.
        /// </summary>
        protected ITemplateRenderer TemplateRenderer { get; }
        /// <summary>
        /// Reference to <see cref="INotificationStore"/>.
        /// </summary>
        protected INotificationStore NotificationStore { get; }
        /// <summary>
        /// Reference to <see cref="IStringLocalizerFactory"/>.
        /// </summary>
        protected IStringLocalizerFactory StringLocalizerFactory { get; }
        /// <summary>
        /// Reference to <see cref="INotificationDataSerializer"/>.
        /// </summary>
        protected INotificationDataSerializer NotificationDataSerializer { get; }
        /// <summary>
        /// Reference to <see cref="INotificationDefinitionManager"/>.
        /// </summary>
        protected INotificationDefinitionManager NotificationDefinitionManager { get; }
        /// <summary>
        /// Reference to <see cref="INotificationSubscriptionManager"/>.
        /// </summary>
        protected INotificationSubscriptionManager NotificationSubscriptionManager { get; }
        /// <summary>
        /// Reference to <see cref="INotificationPublishProviderManager"/>.
        /// </summary>
        protected INotificationPublishProviderManager NotificationPublishProviderManager { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationEventHandler"/> class.
        /// </summary>
        public NotificationEventHandler(
            ICurrentTenant currentTenant,
            ITenantConfigurationCache tenantConfigurationCache,
            IJsonSerializer jsonSerializer,
            ITemplateRenderer templateRenderer,
            IBackgroundJobManager backgroundJobManager,
            IStringLocalizerFactory stringLocalizerFactory,
            IOptions<AbpNotificationsPublishOptions> options,
            INotificationStore notificationStore,
            INotificationDataSerializer notificationDataSerializer,
            INotificationDefinitionManager notificationDefinitionManager,
            INotificationSubscriptionManager notificationSubscriptionManager,
            INotificationPublishProviderManager notificationPublishProviderManager)
        {
            Options = options.Value;
            TenantConfigurationCache = tenantConfigurationCache;
            CurrentTenant = currentTenant;
            JsonSerializer = jsonSerializer;
            TemplateRenderer = templateRenderer;
            BackgroundJobManager = backgroundJobManager;
            StringLocalizerFactory = stringLocalizerFactory;
            NotificationStore = notificationStore;
            NotificationDataSerializer = notificationDataSerializer;
            NotificationDefinitionManager = notificationDefinitionManager;
            NotificationSubscriptionManager = notificationSubscriptionManager;
            NotificationPublishProviderManager = notificationPublishProviderManager;

            Logger = NullLogger<NotificationEventHandler>.Instance;
        }
        #endregion

        #region Utility
        /// <summary>
        /// 给指定租户用户发送 模板通知
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="notification"></param>
        /// <param name="eventData"></param>
        /// <returns></returns>
        protected async virtual Task SendToTenantAsync(
            Guid? tenantId,
            NotificationDefinition notification,
            NotificationEto<NotificationTemplate> eventData)
        {
            using (CurrentTenant.Change(tenantId))
            {
                var providers = Enumerable.Reverse(NotificationPublishProviderManager.Providers);

                // 过滤用户指定提供者
                if (eventData.UseProviders.Any())
                {
                    providers = providers.Where(p => eventData.UseProviders.Contains(p.Name));
                }
                else if (notification.Providers.Any())
                {
                    providers = providers.Where(p => notification.Providers.Contains(p.Name));
                }

                var notificationInfo = new NotificationInfo
                {
                    Name = notification.Name,
                    TenantId = tenantId,
                    Severity = eventData.Severity,
                    Type = notification.NotificationType,
                    ContentType = notification.ContentType,
                    CreationTime = eventData.CreationTime,
                    Lifetime = notification.NotificationLifetime,
                };
                notificationInfo.SetId(eventData.Id);

                var title = notification.DisplayName.Localize(StringLocalizerFactory);
                var message = "";

                try
                {
                    // 由于模板通知受租户影响, 格式化失败的消息将被丢弃.
                    message = await TemplateRenderer.RenderAsync(
                        templateName: eventData.Data.Name,
                        model: eventData.Data.ExtraProperties,
                        cultureName: eventData.Data.Culture,
                        globalContext: new Dictionary<string, object>
                        {
                            // 模板不支持 $ 字符, 改为普通关键字
                            { NotificationKeywords.Name, notification.Name },
                            { NotificationKeywords.FormUser, eventData.Data.FormUser },
                            { NotificationKeywords.Id, eventData.Id },
                            { NotificationKeywords.Title, title.ToString() },
                            { NotificationKeywords.CreationTime, eventData.CreationTime.ToString(Options.DateTimeFormat) },
                        });
                }
                catch (Exception ex)
                {
                    Logger.LogWarning("Formatting template notification failed, message will be discarded, cause :{message}", ex.Message);
                    return;
                }

                var notificationData = new NotificationData();
                notificationData.WriteStandardData(
                    title: title.ToString(),
                    message: message,
                    createTime: eventData.CreationTime,
                    formUser: eventData.Data.FormUser);
                notificationData.ExtraProperties.AddIfNotContains(eventData.Data.ExtraProperties);

                notificationInfo.Data = notificationData;

                var subscriptionUsers = await GerSubscriptionUsersAsync(
                    notificationInfo.Name,
                    eventData.Users,
                    tenantId);

                await PersistentNotificationAsync(
                    notificationInfo,
                    subscriptionUsers,
                    providers);

                if (subscriptionUsers.Any())
                {
                    // 发布通知
                    foreach (var provider in providers)
                    {
                        await PublishToSubscriberAsync(provider, notificationInfo, subscriptionUsers);
                    }
                }
            }
        }


        /// <summary>
        /// 给指定租户用户发送通知
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="notification"></param>
        /// <param name="eventData"></param>
        /// <returns></returns>
        protected async virtual Task SendToTenantAsync(
            Guid? tenantId,
            NotificationDefinition notification,
            NotificationEto<NotificationData> eventData)
        {
            using (CurrentTenant.Change(tenantId))
            {
                var providers = Enumerable.Reverse(NotificationPublishProviderManager.Providers);

                // 过滤用户指定提供者
                if (eventData.UseProviders.Any())
                {
                    providers = providers.Where(p => eventData.UseProviders.Contains(p.Name));
                }
                else if (notification.Providers.Any())
                {
                    providers = providers.Where(p => notification.Providers.Contains(p.Name));
                }

                var notificationInfo = new NotificationInfo
                {
                    Name = notification.Name,
                    CreationTime = eventData.CreationTime,
                    Data = eventData.Data,
                    Severity = eventData.Severity,
                    Lifetime = notification.NotificationLifetime,
                    TenantId = tenantId,
                    Type = notification.NotificationType,
                    ContentType = notification.ContentType,
                };
                notificationInfo.SetId(eventData.Id);

                notificationInfo.Data = NotificationDataSerializer.Serialize(notificationInfo.Data);

                // 获取用户订阅
                var subscriptionUsers = await GerSubscriptionUsersAsync(
                    notificationInfo.Name,
                    eventData.Users,
                    tenantId);

                // 持久化通知
                await PersistentNotificationAsync(
                    notificationInfo,
                    subscriptionUsers,
                    providers);

                if (subscriptionUsers.Any())
                {
                    // 发布订阅通知
                    foreach (var provider in providers)
                    {
                        await PublishToSubscriberAsync(provider, notificationInfo, subscriptionUsers);
                    }
                }
            }
        }

        /// <summary>
        /// 获取用户订阅列表
        /// </summary>
        /// <param name="notificationName">通知名称</param>
        /// <param name="sendToUsers">接收用户列表</param>
        /// <param name="tenantId">租户标识</param>
        /// <returns>用户订阅列表</returns>
        protected async Task<IEnumerable<UserIdentifier>> GerSubscriptionUsersAsync(
            string notificationName,
            IEnumerable<UserIdentifier> sendToUsers,
            Guid? tenantId = null)
        {
            try
            {
                // 获取用户订阅列表
                var userSubscriptions = await NotificationSubscriptionManager.GetUsersSubscriptionsAsync(
                    tenantId,
                    notificationName,
                    sendToUsers);

                return userSubscriptions.Select(us => new UserIdentifier(us.UserId, us.UserName));
            }
            catch (Exception ex)
            {
                Logger.LogWarning("Failed to get user subscription, message will not be received by the user, reason: {message}", ex.Message);
            }

            return new List<UserIdentifier>();
        }

        /// <summary>
        /// 持久化通知并返回订阅用户列表
        /// </summary>
        /// <param name="notificationInfo">通知实体</param>
        /// <param name="subscriptionUsers">订阅用户列表</param>
        /// <param name="sendToProviders">通知发送提供者</param>
        /// <returns>返回订阅者列表</returns>
        protected async Task PersistentNotificationAsync(
            NotificationInfo notificationInfo,
            IEnumerable<UserIdentifier> subscriptionUsers,
            IEnumerable<INotificationPublishProvider> sendToProviders)
        {
            try
            {
                // 持久化通知
                await NotificationStore.InsertNotificationAsync(notificationInfo);

                if (!subscriptionUsers.Any()) return;

                // 持久化用户通知
                await NotificationStore.InsertUserNotificationsAsync(notificationInfo,
                    subscriptionUsers.Select(u => u.UserId));

                if (notificationInfo.Lifetime == NotificationLifetime.OnlyOne)
                {
                    // 一次性通知取消用户订阅
                    await NotificationStore.DeleteUserSubscriptionAsync(
                        notificationInfo.TenantId,
                        subscriptionUsers,
                        notificationInfo.Name);
                }

            }
            catch (Exception ex)
            {
                Logger.LogWarning("Failed to persistent notification failed, reason: {message}", ex.Message);

                foreach (var provider in sendToProviders)
                {
                    // 处理持久化失败进入后台队列
                    await ProcessingFailedToQueueAsync(provider, notificationInfo, subscriptionUsers);
                }
            }
        }



        /// <summary>
        /// 发布订阅者通知
        /// </summary>
        /// <param name="provider">通知发布者</param>
        /// <param name="notificationInfo">通知信息</param>
        /// <param name="subscriptionUsers">订阅用户列表</param>
        /// <returns></returns>
        protected async Task PublishToSubscriberAsync(
            INotificationPublishProvider provider,
            NotificationInfo notificationInfo,
            IEnumerable<UserIdentifier> subscriptionUsers)
        {
            try
            {
                Logger.LogDebug($"Sending notification with provider {provider.Name}");
                var notificationDataMapping = Options.NotificationDataMappings
                    .GetMapItemOrDefault(provider.Name, notificationInfo.Name);

                if (notificationDataMapping != null)
                {
                    notificationInfo.Data = notificationDataMapping.MappingFunc(notificationInfo.Data);
                }

                // 发布通知
                await provider.PublishAsync(notificationInfo, subscriptionUsers);

                Logger.LogDebug($"Send notification {notificationInfo.Name} with provider {provider.Name} was successful");
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Send notification error with provider {provider.Name}");
                Logger.LogWarning($"Error message:{ex.Message}");
                Logger.LogDebug($"Failed to send notification {notificationInfo.Name}. Try to push notification to background job");
                // 发送失败的消息进入后台队列
                await ProcessingFailedToQueueAsync(provider, notificationInfo, subscriptionUsers);
            }
        }


        /// <summary>
        /// 处理失败的消息进入后台队列
        /// </summary>
        /// <param name="provider">通知发布者</param>
        /// <param name="notificationInfo">通知信息</param>
        /// <param name="subscriptionUsers">订阅用户列表</param>
        /// <returns></returns>
        protected async Task ProcessingFailedToQueueAsync(
            INotificationPublishProvider provider,
            NotificationInfo notificationInfo,
            IEnumerable<UserIdentifier> subscriptionUsers)
        {
            try
            {
                //var notificationPublishJobArgs = new NotificationPublishJobArgs(
                //    notificationInfo.GetId(),
                //    provider.GetType().AssemblyQualifiedName,
                //    subscriptionUsers.ToList(),
                //    notificationInfo.TenantId);
                //await BackgroundJobManager.EnqueueAsync(notificationPublishJobArgs);
            }
            catch (Exception ex)
            {
                Logger.LogWarning("Failed to push to background job, notification will be discarded, error cause: {message}", ex.Message);
            }
        }
        #endregion

        #region Event Handler
        [UnitOfWork]
        public async Task HandleEventAsync(NotificationEto<NotificationData> eventData)
        {
            var notification = await NotificationDefinitionManager.GetOrNullAsync(eventData.Name);
            if (notification == null)
            {
                return;
            }

            if (notification.NotificationType == NotificationType.System)
            {
                using (CurrentTenant.Change(null))
                {
                    await SendToTenantAsync(null, notification, eventData);

                    var allActiveTenants = await TenantConfigurationCache.GetTenantsAsync();

                    foreach (var activeTenant in allActiveTenants)
                    {
                        await SendToTenantAsync(activeTenant.Id, notification, eventData);
                    }
                }
            }
            else
            {
                await SendToTenantAsync(eventData.TenantId, notification, eventData);
            }
        }


        [UnitOfWork]
        public async Task HandleEventAsync(NotificationEto<NotificationTemplate> eventData)
        {
            var notification = await NotificationDefinitionManager.GetOrNullAsync(eventData.Name);
            if (notification == null)
            {
                return;
            }

            var culture = eventData.Data.Culture;
            if (culture.IsNullOrWhiteSpace())
            {
                culture = CultureInfo.CurrentCulture.Name;
            }
            using (CultureHelper.Use(culture, culture))
            {
                if (notification.NotificationType == NotificationType.System)
                {
                    using (CurrentTenant.Change(null))
                    {
                        await SendToTenantAsync(null, notification, eventData);

                        var allActiveTenants = await TenantConfigurationCache.GetTenantsAsync();

                        foreach (var activeTenant in allActiveTenants)
                        {
                            await SendToTenantAsync(activeTenant.Id, notification, eventData);
                        }
                    }
                }
                else
                {
                    await SendToTenantAsync(eventData.TenantId, notification, eventData);
                }
            }
        } 
        #endregion
    }
}
