using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Infrastructure.Notification.Common;
using Tiger.Infrastructure.Notification.Core;
using Volo.Abp.Localization;
using Volo.Abp.TextTemplating;

namespace Tiger.Infrastructure.Notification
{
    public class NotificationsCommonNotificationDefinitionProvider : NotificationDefinitionProvider
    {
        public override void Define(INotificationDefinitionContext context)
        {
            var commonGroup = context.AddGroup(
                    NotificationsCommonNotificationNames.GroupName,
                    L("Notifications:Primitives"),
                    allowSubscriptionToClients: false);

            commonGroup.AddNotification(
                name: NotificationsCommonNotificationNames.ExceptionHandling,
                displayName: L("Notifications:ExceptionNotifier"),
                description: L("Notifications:ExceptionNotifier"),
                notificationType: NotificationType.System,
                lifetime: NotificationLifetime.Persistent,
                allowSubscriptionToClients: false)
                // 指定通知提供程序
                .WithProviders(NotificationProviderNames.Emailing)
                // 设定为模板通知
                .WithTemplate(typeof(NotificationsResource), layout: "EmailNotifierLayout")
                .WithTemplate(template =>
                {
                    template.WithVirtualFilePath("/LINGYUN/Abp/Notifications/Templates/ExceptionNotifier", isInlineLocalized: false);
                });

            var tenantsGroup = context.AddGroup(
                    TenantNotificationNames.GroupName,
                    L("Notifications:MultiTenancy"),
                    allowSubscriptionToClients: false);

            tenantsGroup.AddNotification(
                TenantNotificationNames.NewTenantRegistered,
                L("Notifications:NewTenantRegisterd"),
                L("Notifications:NewTenantRegisterd"),
                notificationType: NotificationType.System,
                lifetime: NotificationLifetime.OnlyOne,
                allowSubscriptionToClients: false
                )
                .WithProviders(
                    NotificationProviderNames.SignalR,
                    NotificationProviderNames.Emailing)
                .WithTemplate(typeof(NotificationsResource))
                .WithTemplate(template =>
                {
                    template.WithVirtualFilePath("/LINGYUN/Abp/Notifications/Templates/NewTenantRegisterd", isInlineLocalized: false);
                });

            var usersGroup = context.AddGroup(
                UserNotificationNames.GroupName,
                L("Notifications:Users"));

            usersGroup.AddNotification(
                UserNotificationNames.WelcomeToApplication,
                L("Notifications:WelcomeToApplication"),
                L("Notifications:WelcomeToApplication"),
                notificationType: NotificationType.Application,
                lifetime: NotificationLifetime.OnlyOne,
                allowSubscriptionToClients: true)
                .WithProviders(
                    NotificationProviderNames.SignalR,
                    NotificationProviderNames.Emailing)
                .WithTemplate(typeof(NotificationsResource))
                .WithTemplate(template =>
                {
                    template.WithVirtualFilePath("/LINGYUN/Abp/Notifications/Templates/WelcomeToApplication", isInlineLocalized: false);
                });
        }

        protected LocalizableString L(string name)
        {
            return LocalizableString.Create<NotificationsResource>(name);
        }
    }
}
