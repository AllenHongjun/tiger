using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.Notifications
{
    /// <summary>
    /// 通知管理模块配置选项
    /// </summary>
    public class AbpNotificationsManagementOptions
    {
        /// <summary>
        /// Default: true.
        /// </summary>
        public bool SaveStaticNotificationsToDatabase { get; set; }
        /// <summary>
        /// Default: false.
        /// </summary>
        public bool IsDynamicNotificationsStoreEnabled { get; set; }
        /// <summary>
        /// 缓存刷新时间
        /// default: 30 seconds
        /// </summary>
        public TimeSpan NotificationsCacheRefreshInterval { get; set; }
        /// <summary>
        /// 申请时间戳超时时间
        /// default: 2 minutes
        /// </summary>
        public TimeSpan NotificationsCacheStampTimeOut { get; set; }
        /// <summary>
        /// 时间戳过期时间
        /// default: 30 days
        /// </summary>
        public TimeSpan NotificationsCacheStampExpiration { get; set; }

        public AbpNotificationsManagementOptions()
        {
            SaveStaticNotificationsToDatabase = true;

            NotificationsCacheRefreshInterval = TimeSpan.FromSeconds(30);
            NotificationsCacheStampTimeOut = TimeSpan.FromMinutes(2);

            // 30天过期重新刷新缓存
            NotificationsCacheStampExpiration = TimeSpan.FromDays(30);
        }
    }
}
