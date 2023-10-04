using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Infrastructure.Notification
{
    /// <summary>
    /// 静态通知定义存储
    /// </summary>
    public interface IStaticNotificationDefinitionStore
    {
        Task<NotificationDefinition> GetOrNullAsync(string name);

        Task<IReadOnlyList<NotificationDefinition>> GetNotificationsAsync();

        Task<IReadOnlyList<NotificationGroupDefinition>> GetGroupsAsync();
    }
}
