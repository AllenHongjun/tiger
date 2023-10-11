using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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


    }
}
