using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.System.Platform.Layouts;
using Tiger.Module.System.Platform.Utils;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace Tiger.Module.System.Platform.Menus
{
    public class MenuManager : DomainService
    {
        public MenuManager(
            IMenuRepository menuRepository,
            IUserMenuRepository userMenuRepository,
            IRoleMenuRepository roleMenuRepository)
        {
            MenuRepository=menuRepository;
            UserMenuRepository=userMenuRepository;
            RoleMenuRepository=roleMenuRepository;
        }

        // TODO: 获取 UnitOfWorkManager
        protected IUnitOfWorkManager UnitOfWorkManager { get; set; } 
        //=> LazyGetRequiredService<IUnitOfWorkManager>(ref UnitOfWorkManager);

        protected IMenuRepository MenuRepository { get; }
        protected IUserMenuRepository UserMenuRepository { get; }
        protected IRoleMenuRepository RoleMenuRepository { get; }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="id"></param>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="component"></param>
        /// <param name="displayName"></param>
        /// <param name="redirect"></param>
        /// <param name="description"></param>
        /// <param name="parentId"></param>
        /// <param name="tenantId"></param>
        /// <param name="isPublic"></param>
        /// <returns></returns>
        /// <remarks>
        /// TODO: 增加一个菜单关联的权限字段，根据权限判断是否用户拥有菜单，减少复杂度
        /// </remarks>
        [UnitOfWork]
        public async virtual Task<Menu> CreateAsync(
            Layout layout,
            Guid id,
            string path,
            string name,
            string component,
            string displayName,
            string redirect = "",
            string description = "",
            Guid? parentId = null,
            bool status = true,
            string icon = "",
            Guid? tenantId = null,
            bool isPublic = false
            )
        {
            var code = await GetNextChildCodeAsync(parentId);
            if (code.Length > MenuConsts.MaxCodeLength)
            {
                throw new BusinessException(PlatformErrorCodes.MenuAchieveMaxDepth)
                    .WithData("Depth", MenuConsts.MaxDepth);
            }

            var menu = new Menu(
                id, layout.Id, path, name, code, component, displayName, layout.Framework, redirect,
                description, parentId, status, icon, tenantId)
            {
                IsPublic = isPublic
            };
            await ValidateMenuAsync(menu);
            await MenuRepository.InsertAsync(menu);

            return menu;
        }

        [UnitOfWork]
        public async virtual Task UpdateAsync(Menu menu)
        {
            await ValidateMenuAsync(menu);
            await MenuRepository.UpdateAsync(menu);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async virtual Task DeleteAsync(Guid id)
        {
            // 移除子菜单关联的用户以及角色，移除所有的子菜单
            var children = await FindChildrenAsync(id, true);
            foreach (var child in children)
            {
                await MenuRepository.RemoveAllMembersAsync(child);
                await MenuRepository.RemoveAllRolesAsync(child);
                await MenuRepository.DeleteAsync(child);
            }

            var menu = await MenuRepository.GetAsync(id);
            await MenuRepository.RemoveAllMembersAsync(menu);
            await MenuRepository.RemoveAllRolesAsync(menu);

            await MenuRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 移动菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async virtual Task MoveAsync(Guid id, Guid? parentId)
        {
            var menu = await MenuRepository.GetAsync(id);
            if (menu.ParentId == parentId)
            {
                return;
            }

            var children = await FindChildrenAsync(id, true);
            var oldCode = menu.Code;
            menu.Code = await GetNextChildCodeAsync(parentId);
            menu.ParentId = parentId;

            await ValidateMenuAsync(menu);

            // 生成所有子节点的编码
            foreach (var child in children)
            {
                child.Code = CodeNumberGenerator.AppendCode(menu.Code, CodeNumberGenerator.GetRelativeCode(child.Code, oldCode));
            }
        }

        /// <summary>
        /// 设置用户的菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        public async virtual Task SetUserMenuAsync(Guid userId, IEnumerable<Guid> menuIds)
        {
            using(var unitOfWork = UnitOfWorkManager.Begin())
            {
                var userMenus = await UserMenuRepository.GetListByUserIdAsycn(userId);

                var dels = userMenus.Where(x => !menuIds.Contains(x.Id));
                if (dels.Any())
                {
                    foreach (var del in dels)
                    {
                        await UserMenuRepository.DeleteAsync(del);
                    }
                }

                var adds = menuIds.Where(menuId => !userMenus.Any(x => x.MenuId == menuId));
                if (adds.Any())
                {
                    var addInMenus = adds.Select(menuId => new UserMenu(GuidGenerator.Create(), menuId, userId, CurrentTenant.Id));

                    foreach (var add in addInMenus)
                    {
                        await UserMenuRepository.InsertAsync(add);
                    }
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 设置用户启动菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async virtual Task SetUserStartupMenuAsync(Guid userId, Guid menuId)
        {
            using(var unitOfWork = UnitOfWorkManager.Begin())
            {
                var userMenus = await UserMenuRepository.GetListByUserIdAsycn(userId);

                foreach (var menu in userMenus)
                {
                    menu.Startup = false;
                    if (menu.MenuId.Equals(menuId))
                    {
                        menu.Startup = true;
                    }
                    await UserMenuRepository.UpdateAsync(menu);
                }
                // ABP 6.0 之后才支持 UpdateMany的方法
                await unitOfWork.SaveChangesAsync();
            }
        }

        public async virtual Task SetRoleStartupMenuAsync(string roleName, Guid menuId)
        {
            using (var unitOfWork = UnitOfWorkManager.Begin())
            {
                var roleMenus = await RoleMenuRepository.GetListByRoleNameAsync(roleName);

                foreach (var menu in roleMenus)
                {
                    menu.Startup = false;
                    if (menu.MenuId.Equals(menuId))
                    {
                        menu.Startup = true;
                    }
                    await RoleMenuRepository.UpdateAsync(menu);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async virtual Task SetRoleMenusAsync(string roleName, IEnumerable<Guid> menuIds)
        {
            using (var unitOfWork = UnitOfWorkManager.Begin())
            {
                var roleMenus = await RoleMenuRepository.GetListByRoleNameAsync(roleName);

                // 移除不存在的菜单
                // TODO: 升级框架版本解决未能删除不需要菜单的问题
                // roleMenus.RemoveAll(x => !menuIds.Contains(x.MenuId));
                var dels = roleMenus.Where(x => !menuIds.Contains(x.MenuId));
                if (dels.Any())
                {
                    foreach (var del in dels)
                    {
                        await RoleMenuRepository.DeleteAsync(del);
                    }
                   
                }

                var adds = menuIds.Where(menuId => !roleMenus.Any(x => x.MenuId == menuId));
                if (adds.Any())
                {
                    var addInMenus = adds.Select(menuId => new RoleMenu(GuidGenerator.Create(), menuId, roleName, CurrentTenant.Id));
                    foreach (var add in addInMenus)
                    {
                        await RoleMenuRepository.InsertAsync(add);
                    }
                }

                await unitOfWork.SaveChangesAsync();
            }
        }



        public async virtual Task<string> GetNextChildCodeAsync(Guid? parentId)
        {
            var lastChild = await GetLastChildOrNullAsync(parentId);
            if (lastChild != null)
            {
                return CodeNumberGenerator.CalculateNextCode(lastChild.Code);
            }

            var parentCode = parentId !=null
                ? await GetCodeOrDefaultAsync(parentId.Value)
                : null;

            return CodeNumberGenerator.AppendCode(
                parentCode,
                CodeNumberGenerator.CreateCode(1));
        }

        public async virtual Task<string> GetCodeOrDefaultAsync(Guid id)
        {
            var menu = await MenuRepository.GetAsync(id);
            return menu?.Code;
            //return menu == null ? null : menu.Code;
        }

        /// <summary>
        /// 获取最后一个子节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async virtual Task<Menu> GetLastChildOrNullAsync(Guid? parentId)
        {
            var children = await MenuRepository.GetChildrenAsync(parentId);
            return children.OrderBy(x => x.Code).LastOrDefault();
        }

        public async Task<List<Menu>> FindChildrenAsync(Guid? parentId, bool recursive = false)
        {
            if (!recursive)
            {
                return await MenuRepository.GetChildrenAsync(parentId);
            }

            if (!parentId.HasValue)
            {
                return await MenuRepository.GetListAsync(includeDetails: true);
            }

            var code = await GetCodeOrDefaultAsync(parentId.Value);

            return await MenuRepository.GetAllChildrenWithParentCodeAsync(code, parentId);
        }

        protected async virtual Task ValidateMenuAsync(Menu menu)
        {
            // 查询所有兄弟菜单
            var siblings = (await FindChildrenAsync(menu.ParentId))
                .Where(x => x.Id != menu.Id)
                .ToList();

            if (siblings.Any(x => x.Name == menu.Name))
            {
                throw new BusinessException(PlatformErrorCodes.DuplicateMenu)
                    .WithData("Name", menu.Name);
            }
        }
    }
}
