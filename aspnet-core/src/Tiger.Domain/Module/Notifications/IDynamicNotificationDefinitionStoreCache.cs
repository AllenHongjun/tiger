using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Tiger.Infrastructure.Notification;

namespace Tiger.Module.Notifications
{
    /// <summary>
    /// 动态通知定义 存储缓存
    /// </summary>
    public interface IDynamicNotificationDefinitionStoreCache
    {
        string CacheStamp { get; set; }

        SemaphoreSlim SyncSemaphore { get; }

        DateTime? LastCheckTime { get; set; }

        Task FillAsync(
            List<NotificationDefinitionGroupRecord> webhookGroupRecords,
            List<NotificationDefinitionRecord> webhookRecords);

        NotificationDefinition GetNotificationOrNull(string name);

        IReadOnlyList<NotificationDefinition> GetNotifications();

        IReadOnlyList<NotificationGroupDefinition> GetGroups();
    }
}
