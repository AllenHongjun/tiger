﻿using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.Notifications.Enums;
using Volo.Abp.Localization;

namespace Tiger.Infrastructure.Notification.Internal
{
    /// <summary>
    /// 默认通知定义提供者
    /// </summary>
    internal class DefaultNotificationDefinitionProvider : NotificationDefinitionProvider
    {
        public override void Define(INotificationDefinitionContext context)
        {
            var internalGroup = context.AddGroup(
                    DefaultNotifications.GroupName,
                    L("Notifications:Internal"));

            internalGroup.AddNotification(
                DefaultNotifications.OnsideNotice,
                L("Notifications:OnsideNotice"),
                L("Notifications:OnsideNoticeDesc"),
                notificationType: NotificationType.Application,
                lifetime: NotificationLifetime.Persistent,
                allowSubscriptionToClients: true)
                .WithProviders(NotificationProviderNames.SignalR);
            internalGroup.AddNotification(
                DefaultNotifications.ActivityNotice,
                L("Notifications:ActivityNotice"),
                L("Notifications:ActivityNoticeDesc"),
                notificationType: NotificationType.Application,
                lifetime: NotificationLifetime.Persistent,
                allowSubscriptionToClients: true)
                .WithProviders(NotificationProviderNames.SignalR);
            internalGroup.AddNotification(
                DefaultNotifications.SystemNotice,
                L("Notifications:SystemNotice"),
                L("Notifications:SystemNoticeDesc"),
                notificationType: NotificationType.System,
                lifetime: NotificationLifetime.Persistent,
                allowSubscriptionToClients: true)
                .WithProviders(NotificationProviderNames.SignalR);
        }

        protected LocalizableString L(string name)
        {
            return LocalizableString.Create<NotificationsResource>(name);
        }
    }
}
