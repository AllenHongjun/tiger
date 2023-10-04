using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Localization;

namespace Tiger.Infrastructure.Notification
{
    /// <summary>
    /// 通知定义上下文
    /// </summary>
    public interface INotificationDefinitionContext
    {
        NotificationGroupDefinition AddGroup(
            [NotNull] string name,
            ILocalizableString displayName = null,
            ILocalizableString description = null,
            bool allowSubscriptionToClients = true);

        NotificationGroupDefinition GetGroupOrNull(string name);

        void RemoveGroup(string name);
    }
}
