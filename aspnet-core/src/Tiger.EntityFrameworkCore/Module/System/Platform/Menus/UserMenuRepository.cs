using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.System.Platform.Menus
{
    public class UserMenuRepository : EfCoreRepository<TigerDbContext, UserMenu, Guid>, IUserMenuRepository
    {
        public UserMenuRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async virtual Task<bool> UserHasInMenuAsync(
            Guid userId,
            string menuName,
            CancellationToken cancellationToken = default)
        {
            return await
                (from userMenu in DbContext.Set<UserMenu>()
                 join menu in DbContext.Set<Menu>()
                      on userMenu.MenuId equals menu.Id
                 where userMenu.UserId.Equals(userId)
                 select menu)
                 .AnyAsync(
                    x => x.Name.Equals(menuName),
                    GetCancellationToken(cancellationToken));
        }

        public async virtual Task<List<UserMenu>> GetListByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(x => x.UserId.Equals(userId))
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async virtual Task<Menu> GetStartUpMenuAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var userMenuQuery = DbContext.Set<UserMenu>()
                .Where(x => x.UserId.Equals(userId))
                .Where(x => x.Startup);

            return await
                (from userMenu in userMenuQuery
                 join menu in DbContext.Set<Menu>()
                      on userMenu.MenuId equals menu.Id
                 select menu)
                 .OrderByDescending(x => x.CreationTime)
                 .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        public Task<List<UserMenu>> GetListByUserIdAsycn(Guid userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        
    }
}
