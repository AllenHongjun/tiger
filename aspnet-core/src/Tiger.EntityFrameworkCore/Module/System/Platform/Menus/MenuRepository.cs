using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.SettingManagement;

namespace Tiger.Module.System.Platform.Menus
{
    /// <summary>
    /// 菜单仓储实现
    /// </summary>
    public class MenuRepository : EfCoreRepository<TigerDbContext, Menu, Guid>, IMenuRepository
    {
        public MenuRepository(
            IDbContextProvider<TigerDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task<Menu> FindByNameAsync(string menuName, CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(x => x.Name == menuName)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Menu> FindMainAsync(string framework = "", CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(menu => menu.Framework.Equals(framework) && menu.Path == "/")
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
            throw new NotImplementedException();
        }

        public async Task<List<Menu>> GetAllAsync(string filter = "", string sorting = "Code", bool reverse = false, string framework = "", Guid? parentId = null, Guid? layoutId = null, CancellationToken cancellationToken = default)
        {
            sorting ??= nameof(Menu.Code);
            sorting = reverse ? sorting + " DESC" : sorting;

            return await DbSet
                .WhereIf(parentId.HasValue, x => x.ParentId == parentId)
                .WhereIf(layoutId.HasValue, x => x.LayoutId == layoutId)
                .WhereIf(!framework.IsNullOrWhiteSpace(), x => x.Framework.Equals(framework))
                .WhereIf(!filter.IsNullOrWhiteSpace(), menu =>
                    menu.Path.Contains(filter) || menu.Name.Contains(filter) ||
                    menu.DisplayName.Contains(filter) || menu.Description.Contains(filter) ||
                    menu.Redirect.Contains(filter))
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 根据父级编码查询所有的子菜单
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parentId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <remarks>
        /// 有层级接口的编码巧妙的解决数据库中查询所有子菜单的问题
        /// </remarks>
        public async Task<List<Menu>> GetAllChildrenWithParentCodeAsync(string code, Guid? parentId, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(x => x.Code.StartsWith(code) && x.Id != parentId.Value)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Menu>> GetChildrenAsync(Guid? parentId, CancellationToken cancellationToken = default)
        {   
            return await DbSet
                .Where(x => x.ParentId == parentId)
                .ToListAsync (GetCancellationToken(cancellationToken));
        }

        public async Task<int> GetCountAsync(string filter = "", string framework = "", Guid? parentId = null, Guid? layoutId = null, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .WhereIf(parentId.HasValue, x => x.ParentId == parentId)
                .WhereIf(layoutId.HasValue, x => x.LayoutId == layoutId)
                .WhereIf(!framework.IsNullOrWhiteSpace(), menu => menu.Framework.Equals(framework))
                .WhereIf(!filter.IsNullOrWhiteSpace(), menu =>
                        menu.Path.Contains(filter) || menu.Name.Contains(filter) ||
                        menu.DisplayName.Contains(filter) || menu.Description.Contains(filter) ||
                        menu.Redirect.Contains(filter))
                .CountAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 查询最后一个子菜单
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Menu> GetLastMenuAsync(Guid? parentId = null, CancellationToken cancellationToken = default)
        {   
            return await DbSet
                .Where(x => x.ParentId == parentId)
                .OrderByDescending(x => x.CreationTime)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 根据id数组查询菜单列表
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Menu>> GetListAsync(IEnumerable<Guid> idList, CancellationToken cancellationToken = default)
        {   
            return await DbSet
                .Where( x => idList.Contains(x.Id))
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Menu>> GetListAsync(string filter = "", string sorting = "Code", bool reverse = false, string framework = "", Guid? parentId = null, Guid? layoutId = null, int skipCount = 0, int maxResultCount = 10, CancellationToken cancellationToken = default)
        {
            sorting ??= nameof(Menu.Code);
            sorting = reverse ? sorting + " DESC" : sorting;
            return await DbSet
                .WhereIf(parentId.HasValue, x => x.ParentId == parentId)
                .WhereIf(layoutId.HasValue, x => x.LayoutId == layoutId)
                .WhereIf(!framework.IsNullOrWhiteSpace(), menu => menu.Framework.Equals(framework))
                .WhereIf(!filter.IsNullOrWhiteSpace(), menu =>
                        menu.Path.Contains(filter) || menu.Name.Contains(filter) ||
                        menu.DisplayName.Contains(filter) || menu.Description.Contains(filter) ||
                        menu.Redirect.Contains(filter))
                .OrderBy(sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 查询角色包含的菜单
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="framework"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Menu>> GetRoleMenusAsync(string roles, string framework = "", CancellationToken cancellationToken = default)
        {   
            // 查询当前布局的菜单
            var menuQuery =  DbSet
                .Where(menu => menu.Framework.Equals(framework));

            // 查询角色的菜单
            var roleMenuQuery = DbContext.Set<RoleMenu>()
                .Where(menu => roles.Contains(menu.RoleName));

            return await (from menu in menuQuery
                          join roleMenu in roleMenuQuery
                             on menu.Id equals roleMenu.MenuId
                           select menu)
                           .Union(menuQuery.Where(x => x.IsPublic))
                           .Distinct()
                           .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public Task<Menu> GetUserMenusAsync(Guid userId, string[] roles, string framework = "", CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 移除用户的所有菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task RemoveAllMembersAsync(Menu menu, CancellationToken token = default)
        {   
            var membersQuery = await DbContext.Set<UserMenu>()
                .Where(x => x.MenuId.Equals(menu.Id))
                .ToListAsync(GetCancellationToken(token));

            DbContext.Set<UserMenu>().RemoveRange(membersQuery);
        }

        /// <summary>
        /// 移除角色所有拥有的菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task RemoveAllRolesAsync(Menu menu, CancellationToken token = default)
        {
            var rolesQuery = await DbContext.Set<RoleMenu>()
                .Where(q => q.MenuId == menu.Id)
                .ToListAsync(GetCancellationToken(token));

            DbContext.Set<RoleMenu>().RemoveRange(rolesQuery);
        }
    }
}
