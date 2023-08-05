using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.System.Platform.Datas;
using Tiger.Module.System.Platform.Layouts;
using Tiger.Module.System.Platform.Menus.Dto;
using Tiger.Module.System.Platform.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.ObjectMapping;

namespace Tiger.Module.System.Platform.Menus
{   
    /// <summary>
    /// 菜单
    /// </summary>
    [RemoteService(IsEnabled = false)]
    [Authorize]
    public class MenuAppService : ApplicationService, IMenuAppService
    {
        public MenuAppService(
            //IOptions<DataItemMappingOptions> options,
            IDataRepository dataRepository,
            MenuManager menuManager,
            IMenuRepository menuRepository,
            IUserMenuRepository userMenuRepository,
            IRoleMenuRepository roleMenuRepository,
            ILayoutRepository layoutRepository)
        {
            //DataItemMappingOptions=options.Value;
            DataRepository=dataRepository;
            MenuManager=menuManager;
            MenuRepository=menuRepository;
            UserMenuRepository=userMenuRepository;
            RoleMenuRepository=roleMenuRepository;
            LayoutRepository=layoutRepository;
        }

        //protected DataItemMappingOptions DataItemMappingOptions { get; }
        protected IDataRepository DataRepository { get; }
        protected MenuManager MenuManager { get; }
        protected IMenuRepository MenuRepository { get; }
        protected IUserMenuRepository UserMenuRepository { get; }
        protected IRoleMenuRepository RoleMenuRepository { get; }
        protected ILayoutRepository LayoutRepository { get; }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<MenuDto> CreateAsync(MenuCreateDto input)
        {
            var layout = await LayoutRepository.GetAsync(input.LayoutId);
            //var data = await DataRepository.GetAsync(layout.DataId);

            var menu = await MenuManager.CreateAsync(
                layout,
                GuidGenerator.Create(),
                input.Path,
                input.Name,
                input.Component,
                input.DisplayName,
                input.Redirect,
                input.Description,
                input.ParentId,
                input.Status,
                input.Icon,
                CurrentTenant.Id,
                input.IsPublic);

            //// 利用布局约定的数据字典来校验必须的路由元数据,元数据的加入是为了适配多端路由
            //foreach (var dataItem in data.Items)
            //{
            //    if (!input.Meta.TryGetValue(dataItem.Name, out object meta))
            //    {
            //        if (!dataItem.AllowBeNull)
            //        {
            //            throw new BusinessException(PlatformErrorCodes.MenuMissingMetadata)
            //                .WithData("Name", dataItem.DisplayName)
            //                .WithData("DataName", data.DisplayName);
            //        }
            //        // 是否需要设定默认值
            //        menu.SetProperty(dataItem.Name, dataItem.DefaultValue);
            //    }
            //    else
            //    {
            //        //// 需要检查参数是否有效
            //        //menu.SetProperty(dataItem.Name, DataItemMapping.MapToString(dataItem.ValueType, meta));
            //    }
            //}

            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<Menu, MenuDto>(menu);
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(PlatformPermissions.Menu.Update)]
        public async Task<MenuDto> UpdateAsync(Guid id, MenuUpdateDto input)
        {
            var menu = await MenuRepository.GetAsync(id);

            #region 关联数据字典
            // 利用布局约定的数据字典来校验必须的路由元数据,元数据的加入是为了适配多端路由
            var layout = await LayoutRepository.GetAsync(menu.LayoutId);
            //var data = await DataRepository.GetAsync(layout.DataId);
            //foreach (var dataItem in data.Items)
            //{
            //    if (!input.Meta.TryGetValue(dataItem.Name, out object meta))
            //    {
            //        if (!dataItem.AllowBeNull)
            //        {
            //            throw new BusinessException(PlatformErrorCodes.MenuMissingMetadata)
            //                .WithData("Name", dataItem.DisplayName)
            //                .WithData("DataName", data.DisplayName);
            //        }
            //        // 是否需要设定默认值?
            //        menu.SetProperty(dataItem.Name, dataItem.DefaultValue);
            //    }
            //    else
            //    {
            //        // 与现有的数据做对比
            //        var menuMeta = menu.GetProperty(dataItem.Name);
            //        if (menuMeta != null && menuMeta.Equals(meta))
            //        {
            //            continue;
            //        }
            //        //// 需要检查参数是否有效
            //        //menu.SetProperty(dataItem.Name, DataItemMapping.MapToString(dataItem.ValueType, meta));
            //    }
            //} 
            #endregion

            if (!string.Equals(menu.Name, input.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                menu.Name = input.Name;
            }
            if (!string.Equals(menu.DisplayName, input.DisplayName, StringComparison.InvariantCultureIgnoreCase))
            {
                menu.DisplayName = input.DisplayName;
            }
            if (!string.Equals(menu.Description, input.Description, StringComparison.InvariantCultureIgnoreCase))
            {
                menu.Description = input.Description;
            }
            if (!string.Equals(menu.Path, input.Path, StringComparison.InvariantCultureIgnoreCase))
            {
                menu.Path = input.Path;
            }
            if (!string.Equals(menu.Redirect, input.Redirect, StringComparison.InvariantCultureIgnoreCase))
            {
                menu.Redirect = input.Redirect;
            }
            if (!string.Equals(menu.Component, input.Component, StringComparison.InvariantCultureIgnoreCase))
            {
                menu.Component = input.Component;
            }
            menu.ParentId = input.ParentId;
            menu.Status = input.Status;
            menu.Icon = input.Icon;
            menu.IsPublic = input.IsPublic;

            await MenuManager.UpdateAsync(menu);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<Menu, MenuDto>(menu);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        [Authorize(PlatformPermissions.Menu.Delete)]
        public async Task DeleteAsync(Guid id)
        {   
            var childrens = await MenuRepository.GetChildrenAsync(id);
            if (childrens.Any())
            {
                throw new BusinessException(PlatformErrorCodes.DeleteMenuHaveChildren);
            }

            var menu = await MenuRepository.GetAsync(id);
            await MenuRepository.DeleteAsync(menu);
        }

        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultDto<MenuDto>> GetAllAsync(MenuGetAllInput input)
        {
            var menus = await MenuRepository.GetAllAsync(
                input.Filter,
                input.Sorting,
                input.Reverse,
                input.Framework,
                input.ParentId,
                input.LayoutId);

            return new ListResultDto<MenuDto>(ObjectMapper.Map<List<Menu>, List<MenuDto>>(menus));
        }

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MenuDto> GetAsync(Guid id)
        {
            var menu = await MenuRepository.GetAsync(id);

            return ObjectMapper.Map<Menu, MenuDto>(menu);
        }

        public Task<ListResultDto<MenuDto>> GetCurrentUserMenuListAsync(GetMenuInput input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 分页查询菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<MenuDto>> GetListAsync(MenuGetListInput input)
        {
            var count = await MenuRepository.GetCountAsync(
                input.Filter, 
                input.Framework, 
                input.ParentId, 
                input.LayoutId);

            var menus = await MenuRepository.GetListAsync(input.Filter,
                input.Sorting,
                input.Reverse,
                input.Framework,
                input.ParentId,
                input.LayoutId,
                input.SkipCount,
                input.MaxResultCount);

            return new PagedResultDto<MenuDto>(
                count, 
                ObjectMapper.Map<List<Menu>, List<MenuDto>>(menus));
        }

        /// <summary>
        /// 获取角色菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultDto<MenuDto>> GetRoleMenuListAsync(MenuGetByRoleInput input)
        {
            var menus = await MenuRepository.GetRoleMenusAsync(new string[] { input.Role }, input.Framework);

            var menuDtos = ObjectMapper.Map<List<Menu>, List<MenuDto>>(menus);

            var startupMenu = await RoleMenuRepository
                .GetStartUpMenuAsync(new string[] { input.Role });
            if (startupMenu != null)
            {
                var findMenu = menuDtos.FirstOrDefault(x => x.Id.Equals(startupMenu.Id));

                if (findMenu != null)
                {
                    findMenu.Startup = true;
                }
            }

            return new ListResultDto<MenuDto>(menuDtos);
        }


        public async Task<ListResultDto<MenuDto>> GetUserMenuListAsync(MenuGetByUserInput input)
        {
            var menus = await MenuRepository.GetUserMenusAsync(input.UserId, input.Roles, input.Framework);

            var menuDtos = ObjectMapper.Map<List<Menu>, List<MenuDto>>(menus);

            var startupMenu = await UserMenuRepository.GetStartUpMenuAsync(input.UserId);

            if (startupMenu == null)
            {
                startupMenu = await RoleMenuRepository.GetStartUpMenuAsync(input.Roles);
            }

            if (startupMenu != null)
            {
                var findMenu = menuDtos.FirstOrDefault(x => x.Id.Equals(startupMenu.Id));

                if (findMenu != null)
                {
                    findMenu.Startup = true;
                }
            }

            return new ListResultDto<MenuDto>(menuDtos);
        }

        public Task SetRoleMenusAsync(RoleMenuInput input)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleStartupAsync(Guid id, RoleMenuStartupInput input)
        {
            throw new NotImplementedException();
        }

        public Task SetUserMenusAsync(UserMenuInput input)
        {
            throw new NotImplementedException();
        }

        public Task SetUserStartupAsync(Guid id, UserMenuStartupInput input)
        {
            throw new NotImplementedException();
        }

        
    }
}
