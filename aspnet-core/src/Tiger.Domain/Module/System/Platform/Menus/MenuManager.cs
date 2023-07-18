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

        //protected IUnitOfWorkManager UnitOfWorkManager => LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>();

        protected IMenuRepository MenuRepository { get; }
        protected IUserMenuRepository UserMenuRepository { get; }
        protected IRoleMenuRepository RoleMenuRepository { get; }

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
                description, parentId, tenantId)
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

        protected async virtual Task<string> GetCodeOrDefaultAsync(Guid id)
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

        protected async Task<List<Menu>> FindChildrenAsync(Guid? parentId, bool recursive = false)
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

            if (siblings.Any(x => x.Name == menu.Name)
            {
                throw new BusinessException(PlatformErrorCodes.DuplicateMenu)
                    .WithData("Name", menu.Name);
            }
        }
    }
}
