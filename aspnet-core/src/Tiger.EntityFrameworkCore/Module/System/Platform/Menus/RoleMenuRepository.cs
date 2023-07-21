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
    public class RoleMenuRepository : EfCoreRepository<TigerDbContext, RoleMenu, Guid>, IRoleMenuRepository
    {
        public RoleMenuRepository(
            IDbContextProvider<TigerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async virtual Task<List<RoleMenu>> GetListByRoleNameAsync(string roleName, CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(x => x.RoleName.Equals(roleName))
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async virtual Task<bool> RoleHasInMenuAsync(
            string roleName,
            string menuName,
            CancellationToken cancellationToken = default)
        {
            var menuQuery = DbContext.Set<Menu>().Where(x => x.Name == menuName);

            return await
                (from roleMenu in DbContext.Set<RoleMenu>()
                 join menu in menuQuery
                      on roleMenu.MenuId equals menu.Id
                 select roleMenu)
                 .AnyAsync(x => x.RoleName == roleName,
                    GetCancellationToken(cancellationToken));
        }

        public async virtual Task<Menu> GetStartUpMenuAsync(
            IEnumerable<string> roleNames,
            CancellationToken cancellationToken = default)
        {
            var roleMenuQuery = DbContext.Set<RoleMenu>()
                .Where(x => roleNames.Contains(x.RoleName))
                .Where(x => x.Startup);

            return await
                (from roleMenu in roleMenuQuery
                 join menu in DbContext.Set<Menu>()
                      on roleMenu.MenuId equals menu.Id
                 select menu)
                 .OrderByDescending(x => x.CreationTime)
                 .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        
    }
}
