using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tiger.Module.System.Platform.Menus.Dto;
using Tiger.Module.System.Platform.Menus;
using Tiger.Module.System.Platform;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;
using Volo.Abp;

namespace Tiger.Module.System
{

    /// <summary>
    /// 菜单管理
    /// </summary>
    [RemoteService(Name = PlatformRemoteServiceConsts.RemoteServiceName)]
    [Area("platform")]
    [Route("api/platform/menus")]
    public class MenuController : AbpController, IMenuAppService
    {
        protected IMenuAppService MenuAppService { get; }
        protected IUserRoleFinder UserRoleFinder { get; }

        public MenuController(
            IMenuAppService menuAppService,
            IUserRoleFinder userRoleFinder)
        {
            MenuAppService = menuAppService;
            UserRoleFinder = userRoleFinder;
        }

        /// <summary>
        /// 获取当前用户菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("by-current-user")]
        public async virtual Task<ListResultDto<MenuDto>> GetCurrentUserMenuListAsync(GetMenuInput input)
        {
            return await MenuAppService.GetCurrentUserMenuListAsync(input);
        }

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async virtual Task<MenuDto> GetAsync(Guid id)
        {
            return await MenuAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async virtual Task<ListResultDto<MenuDto>> GetAllAsync(MenuGetAllInput input)
        {
            return await MenuAppService.GetAllAsync(input);
        }

        /// <summary>
        /// 获取菜单分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async virtual Task<PagedResultDto<MenuDto>> GetListAsync(MenuGetListInput input)
        {
            return await MenuAppService.GetListAsync(input);
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async virtual Task<MenuDto> CreateAsync(MenuCreateDto input)
        {
            return await MenuAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async virtual Task<MenuDto> UpdateAsync(Guid id, MenuUpdateDto input)
        {
            return await MenuAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async virtual Task DeleteAsync(Guid id)
        {
            await MenuAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 设置用户菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("by-user")]
        public async virtual Task SetUserMenusAsync(UserMenuInput input)
        {
            await MenuAppService.SetUserMenusAsync(input);
        }

        [HttpPut]
        [Route("startup/{id}/by-user")]
        public async virtual Task SetUserStartupAsync(Guid id, UserMenuStartupInput input)
        {
            await MenuAppService.SetUserStartupAsync(id, input);
        }

        /// <summary>
        /// 获取用户菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("by-user")]
        public async virtual Task<ListResultDto<MenuDto>> GetUserMenuListAsync(MenuGetByUserInput input)
        {
            return await MenuAppService.GetUserMenuListAsync(input);
        }

        [HttpGet]
        [Route("by-user/{userId}/{framework}")]
        public async virtual Task<ListResultDto<MenuDto>> GetUserMenuListAsync(Guid userId, string framework)
        {
            var userRoles = await UserRoleFinder.GetRolesAsync(userId);

            var getMenuByUser = new MenuGetByUserInput
            {
                UserId = userId,
                Roles = userRoles,
                Framework = framework
            };
            return await MenuAppService.GetUserMenuListAsync(getMenuByUser);
        }

        /// <summary>
        /// 设置角色菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("by-role")]
        public async virtual Task SetRoleMenusAsync(RoleMenuInput input)
        {
            await MenuAppService.SetRoleMenusAsync(input);
        }

        [HttpPut]
        [Route("startup/{id}/by-role")]
        public async virtual Task SetRoleStartupAsync(Guid id, RoleMenuStartupInput input)
        {
            await MenuAppService.SetRoleStartupAsync(id, input);
        }

        /// <summary>
        /// 获取角色菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("by-role")]
        public async virtual Task<ListResultDto<MenuDto>> GetRoleMenuListAsync(MenuGetByRoleInput input)
        {
            return await MenuAppService.GetRoleMenuListAsync(input);
        }

        [HttpGet]
        [Route("by-role/{role}/{framework}")]
        public async virtual Task<ListResultDto<MenuDto>> GetRoleMenuListAsync(string role, string framework)
        {
            return await MenuAppService.GetRoleMenuListAsync(new MenuGetByRoleInput
            {
                Role = role,
                Framework = framework
            });
        }
    }
}
