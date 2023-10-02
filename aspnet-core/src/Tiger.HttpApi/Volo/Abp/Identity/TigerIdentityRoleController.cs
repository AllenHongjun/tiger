using Microsoft.AspNetCore.Http;
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
        #region Ctor
        protected ITigerIdentityRoleAppService RoleAppService { get; }
        public TigerIdentityRoleController(ITigerIdentityRoleAppService roleAppService)
        {
            RoleAppService = roleAppService;
        } 
        #endregion

        #region 角色
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
        /// xlsx 导入模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("import-xlxs-template")]
        public async Task<IActionResult> ImportRoleTemplateAsync()
        {
            GetIdentityRolesInput input = new GetIdentityRolesInput()
            {
                MaxResultCount = 1
            };
            return await RoleAppService.ExportRolesToXlsxAsync(input);
        }

        /// <summary>
        /// 从xlsx 导入角色
        /// </summary>
        /// <param name="importexcelfile"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("import-from-xlsx")]
        public async Task ImportRoleFromXlsxAsync(IFormFile importexcelfile)
        {
            await RoleAppService.ImportRoleFromXlsxAsync(importexcelfile);
        }

        /// <summary>
        /// 导出角色到 xlsx
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("export-to-xlsx")]
        public async Task<IActionResult> ExportRolesToXlsxAsync(GetIdentityRolesInput input)
        {
            input.MaxResultCount = int.MaxValue;
            return await RoleAppService.ExportRolesToXlsxAsync(input);
        }


        /// <summary>
        /// 移动当前角色用户到目标角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="targetRoleId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{roleId}/move-all-users/{targetRoleId}")]
        public async Task MoveAllUsers(Guid roleId, Guid? targetRoleId)
        {
            await RoleAppService.MoveAllUsers(roleId, targetRoleId);
        } 
        #endregion

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
