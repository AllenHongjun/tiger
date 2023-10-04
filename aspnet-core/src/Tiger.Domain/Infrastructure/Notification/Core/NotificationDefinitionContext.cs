using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Localization;
using Volo.Abp;
using JetBrains.Annotations;

namespace Tiger.Infrastructure.Notification.Core
{
    /// <summary>
    /// 通知定义上下文
    /// </summary>
    public class NotificationDefinitionContext : INotificationDefinitionContext
    {
        internal Dictionary<string, NotificationGroupDefinition> Groups { get; }

        public NotificationDefinitionContext()
        {
            Groups = new Dictionary<string, NotificationGroupDefinition>();
        }

        public NotificationGroupDefinition AddGroup(
            [NotNull] string name,
            ILocalizableString displayName = null,
            ILocalizableString description = null,
            bool allowSubscriptionToClients = true)
        {
            Check.NotNull(name, nameof(name));

            if (Groups.ContainsKey(name))
            {
                throw new AbpException($"There is already an existing notification group with name: {name}");
            }

            return Groups[name] = new NotificationGroupDefinition(name, displayName, description, allowSubscriptionToClients);
        }

        public NotificationGroupDefinition GetGroupOrNull(string name)
        {
            Check.NotNull(name, nameof(name));

            if (!Groups.ContainsKey(name))
            {
                return null;
            }

            return Groups[name];
        }

        public void RemoveGroup(string name)
        {
            Check.NotNull(name, nameof(name));

            if (!Groups.ContainsKey(name))
            {
                throw new AbpException($"Undefined notification group: '{name}'.");
            }

            Groups.Remove(name);
        }
    }
}
