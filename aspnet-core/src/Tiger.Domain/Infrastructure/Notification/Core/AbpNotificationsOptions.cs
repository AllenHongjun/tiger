using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Collections;

namespace Tiger.Infrastructure.Notification
{
    public class AbpNotificationsOptions
    {
        /// <summary>
        /// 自定义通知集合
        /// </summary>
        public ITypeList<INotificationDefinitionProvider> DefinitionProviders { get; }

        public HashSet<string> DeletedNotifications { get; }

        public HashSet<string> DeletedNotificationGroups { get; }

        public AbpNotificationsOptions()
        {
            DefinitionProviders = new TypeList<INotificationDefinitionProvider>();

            DeletedNotifications = new HashSet<string>();
            DeletedNotificationGroups = new HashSet<string>();
        }
    }
}
