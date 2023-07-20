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
    public class UserFavoriteMenuRepository : EfCoreRepository<TigerDbContext, UserFavoriteMenu, Guid>,
    IUserFavoriteMenuRepository
    {
        public UserFavoriteMenuRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async virtual Task<List<UserFavoriteMenu>> GetListByMenuIdAsync(
        Guid menuId,
        CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(x => x.MenuId == menuId)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async virtual Task<List<UserFavoriteMenu>> GetFavoriteMenusAsync(
            Guid userId,
            string framework = null,
            Guid? menuId = null,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(x => x.UserId == userId)
                .WhereIf(menuId.HasValue, x => x.MenuId == menuId)
                .WhereIf(!framework.IsNullOrWhiteSpace(), x => x.Framework == framework)
                .OrderBy(x => x.CreationTime)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async virtual Task<bool> CheckExistsAsync(
            string framework,
            Guid userId,
            Guid menuId,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .AnyAsync(x =>
                    x.Framework == framework && x.UserId == userId && x.MenuId == menuId,
                    GetCancellationToken(cancellationToken));
        }

        public async virtual Task<UserFavoriteMenu> FindByUserMenuAsync(
            Guid userId,
            Guid menuId,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(x => x.UserId == userId && x.MenuId == menuId)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        
    }
}
