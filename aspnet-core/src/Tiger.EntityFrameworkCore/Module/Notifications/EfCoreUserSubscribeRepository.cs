using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.Notifications
{
    /// <summary>
    /// 用户订阅仓储实现
    /// </summary>
    public class EfCoreUserSubscribeRepository : EfCoreRepository<TigerDbContext, UserSubscribe, long>,
        IUserSubscribeRepository, ITransientDependency
    {
        public EfCoreUserSubscribeRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task DeleteUserSubscriptionAsync(IEnumerable<UserSubscribe> userSubscribes, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUserSubscriptionAsync(string notificationName, IEnumerable<Guid> userIds, CancellationToken cancellationToken = default)
        {
            //await DeleteManyAsync(userSubscribes);
            throw new NotImplementedException();
        }

        public async Task DeleteUserSubscriptionAsync(string notificationName, CancellationToken cancellationToken = default)
        {
            var userSubscribes = await DbContext.Set<UserSubscribe>()
                .Where(x => x.NotificationName.Equals(notificationName))
                .ToListAsync(GetCancellationToken(cancellationToken));
             DbContext.Set<UserSubscribe>().RemoveRange(userSubscribes);
        }

        public async Task<long> GetCountAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<UserSubscribe>()
                .Distinct()
                .Where(x => x.UserId.Equals(userId))
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<UserSubscribe> GetUserSubscribeAsync(string notificationName, Guid userId, CancellationToken cancellationToken = default)
        {
            var userSubScribe = await DbContext.Set<UserSubscribe>()
                .Where(x => x.UserId.Equals(userId) && x.NotificationName.Equals(notificationName))
                .AsNoTracking()
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
            return userSubScribe;
        }

        public async Task<List<string>> GetUserSubscribesAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<UserSubscribe>()
                .Where(x => x.UserId.Equals(userId))
                .AsNoTracking()
                .Select(x => x.NotificationName)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Guid>> GetUserSubscribesAsync(string notificationName, CancellationToken cancellationToken = default)
        {
            var subScriberUsers = await DbContext.Set<UserSubscribe>()
                .Where(x => x.NotificationName.Equals(notificationName))
                .Select(x => x.UserId)
                .ToListAsync(GetCancellationToken(cancellationToken));
            return subScriberUsers;
        }


        public async Task<List<UserSubscribe>> GetUserSubscribesAsync(
            Guid userId, string sorting = "Id", int skipCount = 1, 
            int maxResultCount = 10, CancellationToken cancellationToken = default)
        {
            var userSubScribes = await DbContext.Set<UserSubscribe>()
                .Distinct()
                .Where(x => x.UserId.Equals(userId))
                .OrderBy(sorting ?? nameof(UserSubscribe.Id))
                .PageBy(skipCount, maxResultCount)
                .AsNoTracking()
                .ToListAsync(GetCancellationToken(cancellationToken));

            return userSubScribes;
        }

        public async Task<List<UserSubscribe>> GetUserSubscribesByNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            var userSubscribes = await DbContext.Set<UserSubscribe>()
                .Distinct()
                .Where(x => x.UserName.Equals(userName))
                .AsNoTracking()
                .ToListAsync(GetCancellationToken(cancellationToken));  

            return userSubscribes;
        }

        public async Task<List<UserSubscribe>> GetUserSubscriptionsAsync(
            string notificationName, 
            IEnumerable<Guid> userIds = null, 
            CancellationToken cancellationToken = default)
        {
            var userSubscribes = await DbContext.Set<UserSubscribe>()
                .Distinct()
                .Where(x => x.NotificationName.Equals(notificationName))
                .WhereIf(userIds.Any() == true, x => userIds.Contains(x.UserId))
                .AsNoTracking()
                .ToListAsync(GetCancellationToken(cancellationToken));

            return userSubscribes;
        }

        public async Task InsertUserSubscriptionAsync(IEnumerable<UserSubscribe> userSubscribes, CancellationToken cancellationToken = default)
        {
            await DbContext.Set<UserSubscribe>()
                .AddRangeAsync(userSubscribes, GetCancellationToken(cancellationToken));
        }

        public async Task<bool> UserSubscribeExistsAsync(string notificationName, Guid userId, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<UserSubscribe>()
                .AnyAsync(x => x.UserId.Equals(userId) && x.NotificationName.Equals(notificationName),
                GetCancellationToken(cancellationToken));
        }
    }
}
