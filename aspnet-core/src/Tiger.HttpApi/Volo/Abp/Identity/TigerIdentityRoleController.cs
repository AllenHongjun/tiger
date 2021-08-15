using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Auditing;

namespace Volo.Abp.Identity
{
    [ApiExplorerSettings(GroupName = "mom")]
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
    }
}
