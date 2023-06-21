using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity.OrganizationUnits.Dto;
using Tiger.Volo.Abp.Identity.Users;
using Tiger.Volo.Abp.Identity.Users.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace Tiger.Volo.Abp.Identity
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area("identity")]
    [ControllerName("User")]
    [Route("api/identity/users")]
    public class TigerIdentityUserController : AbpController, ITigerIdentityUserAppService
    {
        protected ITigerIdentityUserAppService _userAppService;
        private readonly IIdentityUserAppService _identityUserAppService;
        protected readonly IPermissionAppService _permissionAppService;


        public TigerIdentityUserController(ITigerIdentityUserAppService userAppService, IIdentityUserAppService identityUserAppService)
        {
            _userAppService = userAppService;
            _identityUserAppService = identityUserAppService;
        }



        /// <summary>
        /// 将用户关联组织
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ouIds"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{userId}/add-to-organizations")]
        public Task AddToOrganizationUnitsAsync(Guid userId, List<Guid> ouIds)
        {
            return _userAppService.AddToOrganizationUnitsAsync(userId, ouIds);
        }

        /// <summary>
        /// 添加用户同时关联组织
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-to-organizations")]
        public Task<IdentityUserDto> CreateAsync(IdentityUserOrgCreateDto input)
        {
            return _userAppService.CreateAsync(input);
        }

        /// <summary>
        /// 获取用户关联的组织单元
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/organizations")]
        public Task<ListResultDto<OrganizationUnitDto>> GetListOrganizationUnitsAsync(Guid id, bool includeDetails = false)
        {
            return _userAppService.GetListOrganizationUnitsAsync(id, includeDetails);
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/update-to-organizations")]
        public Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserOrgUpdateDto input)
        {
            return _userAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPut]
        [Route("{id}/change-password")]
        public Task ChangePasswordAsync(Guid id, IdentityUserSetPasswordInput input)
        {
            return _userAppService.ChangePasswordAsync(id, input);
        }

        /// <summary>
        /// 锁定用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="seconds">锁定时长单位 秒</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/lock")]
        public Task LockAsync(Guid id, int seconds)
        {
            return _userAppService.LockAsync(id, seconds);
        }

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPut]
        [Route("{id}/unlock")]
        public Task UnlockAsync(Guid id)
        {
            return (_userAppService.UnlockAsync(id));
        }
    }
}
