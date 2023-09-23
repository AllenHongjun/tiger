using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity.ClaimTypes.Dto;
using Tiger.Volo.Abp.Identity.OrganizationUnits.Dto;
using Tiger.Volo.Abp.Identity.Roles;
using Tiger.Volo.Abp.Identity.Roles.Dto;
using Tiger.Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Auditing;
using Volo.Abp.Http;

namespace Volo.Abp.Identity
{
    /// <summary>
    /// 角色
    /// </summary>
    [ApiExplorerSettings(GroupName = "admin")]
    [DisableAuditing]
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area("identity")]
    [ControllerName("Role")]
    [Route("api/identity/roles")]
    public class TigerIdentityRoleController : AbpController, ITigerIdentityRoleAppService
    {
        protected ITigerIdentityRoleAppService RoleAppService { get; }
        public TigerIdentityRoleController(ITigerIdentityRoleAppService roleAppService)
        {
            RoleAppService = roleAppService;
        }


        

        /// <summary>
        /// 查询角色列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        [HttpGet]
        [Route("search")]
        public Task<PagedResultDto<IdentityRoleDto>> GetListAsync(GetIdentityRolesInput input)
        {
            return RoleAppService.GetListAsync(input);
        }

        /// <summary>
        /// 导出角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("role-export")]
        public async Task<FileResult> ExportRolesToXlsxAsync(GetIdentityRolesInput input)
        {
            return await RoleAppService.ExportRolesToXlsxAsync(input);
        }


        /// <summary>
        /// 移动当前角色用户到目标角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="targetRoleId"></param>
        /// <param name="cancelAssign"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{roleId}/move-all-users")]
        public async Task MoveAllUsers(Guid roleId, Guid targetRoleId, bool cancelAssign)
        {
            await RoleAppService.MoveAllUsers(roleId, targetRoleId, cancelAssign);
        }

        #region 角色关联组织
        /// <summary>
        /// 将角色关联组织
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="ouId"></param>
        /// <returns></returns>
        [DisableAuditing]
        [HttpPost]
        [Route("{roleId}/add-to-organization/{ouId}")]
        public virtual Task AddToOrganizationUnitAsync(Guid roleId, Guid ouId)
        {
            return RoleAppService.AddToOrganizationUnitAsync(roleId, ouId);
        }

        /// <summary>
        /// 添加角色关联组织单元
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-to-organization")]
        public Task<IdentityRoleDto> CreateAsync(IdentityRoleOrgCreateDto input)
        {
            return RoleAppService.CreateAsync(input);
        }




        #endregion

        #region 声明管理

        /// <summary>
        /// 添加角色声明
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{roleId}/create-claim")]
        public async Task AddClaimAsync(Guid roleId, IdentityRoleClaimCreateDto input)
        {
            await RoleAppService.AddClaimAsync(roleId, input);
        }

        /// <summary>
        /// 更新角色声明
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{roleId}/update-claim")]
        public async Task UpdateClaimAsync(Guid roleId, IdentityRoleClaimUpdateDto input)
        {
            await RoleAppService.UpdateClaimAsync(roleId, input);
        }

        /// <summary>
        /// 删除角色声明
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{roleId}/delete-claim")]
        public async Task DeleteClaimAsync(Guid roleId, IdentityRoleClaimDeleteDto input)
        {
            await RoleAppService.DeleteClaimAsync(roleId, input);
        }

        /// <summary>
        /// 获取角色声明
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{roleId}/claims")]
        public async Task<ListResultDto<IdentityRoleClaimDto>> GetClaimsAsync(Guid roleId)
        {
            return await RoleAppService.GetClaimsAsync(roleId);
        }

        


        #endregion
    }
}
