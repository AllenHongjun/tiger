using JetBrains.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tiger.Infrastructure.Notification
{
    /// <summary>
    /// 通知定义管理器
    /// </summary>
    public interface INotificationDefinitionManager
    {
        [NotNull]
        Task<NotificationDefinition> GetAsync([NotNull] string name);

        Task<IReadOnlyList<NotificationDefinition>> GetNotificationsAsync();

        Task<NotificationDefinition> GetOrNullAsync(string name);

        Task<IReadOnlyList<NotificationGroupDefinition>> GetGroupsAsync();
    }
}
