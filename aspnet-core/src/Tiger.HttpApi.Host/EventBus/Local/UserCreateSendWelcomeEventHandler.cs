using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiger.Infrastructure.Notification;
using Tiger.Infrastructure.Notification.Core;
using Tiger.Module.Notifications.Enums;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Users;

namespace Tiger.EventBus.Local
{

    /// <summary>
    /// 用户创建发送欢迎 事件处理程序
    /// </summary>
    public class UserCreateSendWelcomeEventHandler
        : ILocalEventHandler<EntityCreatedEventData<UserEto>>, ITransientDependency
    {
        #region 构造函数
        private readonly INotificationSender _notificationSender;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;

        public UserCreateSendWelcomeEventHandler(INotificationSender notificationSender, INotificationSubscriptionManager notificationSubscriptionManager)
        {
            _notificationSender=notificationSender;
            _notificationSubscriptionManager=notificationSubscriptionManager;
        }
        #endregion

        /// <summary>
        /// 订阅内置通知
        /// </summary>
        /// <param name="userIdentifier"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        private async Task SubscribeInternalNotifers(UserIdentifier userIdentifier,
            Guid? tenantId = null)
        {
            // 订阅内置通知
            await _notificationSubscriptionManager
                .SubscribeAsync(tenantId, userIdentifier, DefaultNotifications.SystemNotice);
            await _notificationSubscriptionManager
                .SubscribeAsync(tenantId,userIdentifier, DefaultNotifications.OnsideNotice);
            await _notificationSubscriptionManager
                .SubscribeAsync(tenantId, userIdentifier, DefaultNotifications.ActivityNotice);

            // 订阅用户欢迎信息

            await _notificationSubscriptionManager
                .SubscribeAsync(tenantId, userIdentifier, UserNotificationNames.WelcomeToApplication);
        }


        /// <summary>
        /// 用户
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task HandleEventAsync(EntityCreatedEventData<UserEto> eventData)
        {
            var userIdentifer = new UserIdentifier(eventData.Entity.Id, eventData.Entity.UserName);

            // 订阅用户欢迎消息
            await SubscribeInternalNotifers(userIdentifer, eventData.Entity.TenantId);

            // 给用户发送欢迎消息
            await _notificationSender.SendNofiterAsync(
                UserNotificationNames.WelcomeToApplication,
                new NotificationTemplate(
                    UserNotificationNames.WelcomeToApplication,
                    formUser: eventData.Entity.UserName,
                    data: new Dictionary<string, object>
                    {
                        {"name", eventData.Entity.UserName}
                    }),
                userIdentifer,
                eventData.Entity.TenantId,
                NotificationSeverity.Info
                );

        }
    }
}
