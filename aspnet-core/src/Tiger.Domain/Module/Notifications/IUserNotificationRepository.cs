using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiger.Module.Notifications.Enums;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.Notifications
{
    /// <summary>
    /// 用户通知仓储接口
    /// </summary>
    public interface IUserNotificationRepository : IBasicRepository<UserNotification, long>
    {
        Task<bool> AnyAsync(
            Guid userId,
            long notificationId,
            CancellationToken cancellationToken = default);

        Task<UserNotificationInfo> GetByIdAsync(
            Guid userId,
            long notificationId,
            CancellationToken cancellationToken = default);

        Task<List<UserNotification>> GetListAsync(
            Guid userId,
            IEnumerable<long> notificationIds,
            CancellationToken cancellationToken = default);

        Task<List<UserNotificationInfo>> GetNotificationsAsync(
        Guid userId,
            NotificationReadStatus? readStatus = null,
            int maxResultCount = 10,
            CancellationToken cancellationToken = default);

        Task<int> GetCountAsync(
            Guid userId,
        string filter = "",
            NotificationReadStatus? readStatus = null,
            CancellationToken cancellationToken = default);

        Task<List<UserNotificationInfo>> GetListAsync(
            Guid userId,
            string filter = "",
            string sorting = nameof(Notification.CreationTime),
            NotificationReadStatus? readStatus = null,
            int skipCount = 0,
            int maxResultCount = 10,
            CancellationToken cancellationToken = default);

    }
}
