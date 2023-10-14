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
        //protected ITenantConfigurationCache TenantConfigurationCache { get; }
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
            //ITenantConfigurationCache tenantConfigurationCache,
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
            //TenantConfigurationCache = tenantConfigurationCache;
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

        public Task HandleEventAsync(NotificationEto<NotificationData> eventData)
        {
            return Task.CompletedTask;
            throw new System.NotImplementedException();
        }

        public Task HandleEventAsync(NotificationEto<NotificationTemplate> eventData)
        {
            return Task.CompletedTask;
            throw new System.NotImplementedException();
        }
    }
}
