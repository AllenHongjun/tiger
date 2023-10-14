using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Tiger.EntityFrameworkCore;
using Tiger.Module.Notifications.Enums;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.Notifications
{
    /// <summary>
    /// 用户通知仓储实现
    /// </summary>
    public class EfCoreUserNotificationRepository : EfCoreRepository<TigerDbContext, UserNotification, long>,
        IUserNotificationRepository, ITransientDependency
    {
        public EfCoreUserNotificationRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<bool> AnyAsync(Guid userId, long notificationId, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<UserNotification>()
                .AnyAsync(x => x.NotificationId.Equals(notificationId) && x.UserId.Equals(userId),
                GetCancellationToken(cancellationToken));
        }

        public async Task<UserNotificationInfo> GetByIdAsync(
            Guid userId, long notificationId, 
            CancellationToken cancellationToken = default)
        {
            var userNotifilerQuery = DbContext.Set<UserNotification>()
                .Where(x => x.UserId.Equals(userId));

            var notifilerQuery = from un in userNotifilerQuery
                                 join n in DbContext.Set<Notification>()
                                    on un.NotificationId equals n.NotificationId
                                 where n.NotificationId == notificationId
                                 select new UserNotificationInfo
                                 {
                                     Id = n.NotificationId,
                                     TenantId = n.TenantId,
                                     Name = n.NotificationName,
                                     ExtraProperties = n.ExtraProperties,
                                     CreationTime = n.CreationTime,
                                     NotificationTypeName = n.NotificationTypeName,
                                     Severity = n.Severity,
                                     ReadStatus = un.ReadStatus,
                                     Type = n.Type,
                                     ContentType = n.ContentType
                                 };

            return await notifilerQuery
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<int> GetCountAsync(Guid userId, string filter = "", NotificationReadStatus? readStatus = null, CancellationToken cancellationToken = default)
        {
            var notifilerQuery = from un in DbContext.Set<UserNotification>()
                                 join n in DbContext.Set<Notification>()
                                    on un.NotificationId equals n.NotificationId
                                 where un.UserId == userId
                                 select new UserNotificationInfo
                                 {
                                     Id = n.NotificationId,
                                     TenantId = n.TenantId,
                                     Name = n.NotificationName,
                                     ExtraProperties = n.ExtraProperties,
                                     CreationTime = n.CreationTime,
                                     NotificationTypeName = n.NotificationTypeName,
                                     Severity = n.Severity,
                                     ReadStatus = un.ReadStatus,
                                     Type = n.Type,
                                     ContentType = n.ContentType
                                 };

            return await notifilerQuery
            .WhereIf(readStatus.HasValue, x => x.ReadStatus == readStatus.Value)
                .WhereIf(!filter.IsNullOrWhiteSpace(), nf =>
                    nf.Name.Contains(filter) ||
                    nf.NotificationTypeName.Contains(filter))
                .CountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<UserNotification>> GetListAsync(
            Guid userId, 
            IEnumerable<long> notificationIds, 
            CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<UserNotification>()
                .Where(x => x.UserId.Equals(userId) && notificationIds.Contains(x.NotificationId))
                .ToListAsync();
        }

        public async Task<List<UserNotificationInfo>> GetListAsync(
            Guid userId, string filter = "", string sorting = "CreationTime", 
            NotificationReadStatus? readStatus = null, int skipCount = 0, 
            int maxResultCount = 10, CancellationToken cancellationToken = default)
        {
            var userNotifilerQuery = DbContext.Set<UserNotification>()
                .Where(x => x.UserId.Equals(userId))
                .WhereIf(readStatus.HasValue, x => x.ReadStatus == readStatus);

            var notifilerQuery = from un in userNotifilerQuery
                                 join n in DbContext.Set<Notification>()
                                    on un.NotificationId equals n.NotificationId
                                 select new UserNotificationInfo
                                 {
                                     Id = n.NotificationId,
                                     TenantId = n.TenantId,
                                     Name = n.NotificationName,
                                     ExtraProperties = n.ExtraProperties,
                                     CreationTime = n.CreationTime,
                                     NotificationTypeName = n.NotificationTypeName,
                                     Severity = n.Severity,
                                     ReadStatus = un.ReadStatus,
                                     Type = n.Type,
                                     ContentType = n.ContentType,
                                 };

            return await notifilerQuery
                .OrderBy(nameof(Notification.CreationTime) + " DESC")
                .PageBy(skipCount, maxResultCount)
                .AsNoTracking()
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public Task<List<UserNotificationInfo>> GetNotificationsAsync(Guid userId, NotificationReadStatus? readStatus = null, int maxResultCount = 10, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
