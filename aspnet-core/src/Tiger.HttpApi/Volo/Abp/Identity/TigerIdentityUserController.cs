using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        #region Ctor
        protected ITigerIdentityUserAppService _userAppService;
        private readonly IIdentityUserAppService _identityUserAppService;
        protected readonly IPermissionAppService _permissionAppService;


        public TigerIdentityUserController(ITigerIdentityUserAppService userAppService, IIdentityUserAppService identityUserAppService)
        {
            _userAppService = userAppService;
            _identityUserAppService = identityUserAppService;
        } 
        #endregion

        #region User
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<PagedResultDto<IdentityUserDto>> GetListAsync(IdentityUserGetListInput input)
        {
            return await _userAppService.GetListAsync(input);
        }


        /// <summary>
        /// 从xlsx导入
        /// </summary>
        /// <param name="importExcelFile"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("import-from-xlsx")]
        public async Task ImportUserFromXlsxAsync(IFormFile importExcelFile)
        {
            await _userAppService.ImportUserFromXlsxAsync(importExcelFile);
        }


        /// <summary>
        /// 将用户导出xlsx
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("export-to-xlsx")]
        public async Task<IActionResult> ExportUsersToXlsxAsync(IdentityUserGetListInput input)
        {
            return await _userAppService.ExportUsersToXlsxAsync(input);
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
        #endregion

        #region User Relate Organizations
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
        #endregion

        #region UserClaims

        [HttpGet]
        [Route("{id}/claims")]
        public async Task<ListResultDto<IdentityUserClaimDto>> GetClaimsAsync(Guid id)
        {
            return await _userAppService.GetClaimsAsync(id);
        }

        [HttpPost]
        [Route("{id}/create-claim")]
        public async Task AddClaimAsync(Guid id, IdentityUserClaimCreateDto input)
        {
            await _userAppService.AddClaimAsync(id, input);
        }

        [HttpPut]
        [Route("{id}/update-claim")]
        public async Task UpdateClaimAsync(Guid id, IdentityUserClaimUpdateDto input)
        {
            await _userAppService.UpdateClaimAsync(id, input);
        }

        [HttpPost]
        [Route("{id}/delete-claim")]
        public async Task DeleteClaimAsync(Guid id, IdentityUserClaimDeleteDto input)
        {
            await _userAppService.DeleteClaimAsync(id, input);
        }

        #endregion
    }
}
