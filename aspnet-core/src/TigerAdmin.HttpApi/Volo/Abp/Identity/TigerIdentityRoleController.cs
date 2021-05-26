using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Volo.Abp.Identity
{
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

        [HttpPost]
        [Route("{roleId}/add-to-organization/{ouId}")]
        public virtual Task AddToOrganizationUnitAsync(Guid roleId, Guid ouId)
        {
            return RoleAppService.AddToOrganizationUnitAsync(roleId, ouId);
        }

        [HttpPost]
        [Route("create-to-organization")]
        public Task<IdentityRoleDto> CreateAsync(IdentityRoleOrgCreateDto input)
        {
            return RoleAppService.CreateAsync(input);
        }
    }
}
