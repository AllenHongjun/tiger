using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.Notifications
{
    /// <summary>
    /// 通知仓储实现
    /// </summary>
    public class EfCoreNotificationRepository : EfCoreRepository<TigerDbContext, Notification, long>, INotificationRepository, ITransientDependency
    {
        public EfCoreNotificationRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<Notification> GetByIdAsync(long notificationId, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<Notification>()
                .Where(x => x.NotificationId.Equals(notificationId))
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Notification>> GetExpritionAsync(int batchCount, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<Notification>()
                .Where(x => x.ExpirationTime < DateTime.Now)
                .OrderBy(x => x.ExpirationTime)
                .Take(batchCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
