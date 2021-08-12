using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity
{
    [RemoteService(IsEnabled = false)]
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IIdentityRoleAppService),
        typeof(IdentityRoleAppService),
        typeof(ITigerIdentityRoleAppService),
        typeof(TigerIdentityRoleAppService))]
    public class TigerIdentityRoleAppService : IdentityRoleAppService, ITigerIdentityRoleAppService
    {
        //private IStringLocalizer<HelloAbpResource> _localizer;
        protected OrganizationUnitManager OrgManager { get; }
        public TigerIdentityRoleAppService(IdentityRoleManager roleManager,
            IIdentityRoleRepository roleRepository,
            //IStringLocalizer<HelloAbpResource> localizer,
            OrganizationUnitManager orgManager) : base(roleManager, roleRepository)
        {
            //_localizer = localizer;
            OrgManager = orgManager;
        }

        /// <summary>
        /// 角色关联组织(一个角色之关联一个组织)
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="ouId"></param>
        /// <returns></returns>
        //[Authorize(TigerIdentityPermissions.Roles.AddOrganizationUnitRole)]
        public Task AddToOrganizationUnitAsync(Guid roleId, Guid ouId)
        {
            return OrgManager.AddRoleToOrganizationUnitAsync(roleId, ouId);
        }

        /// <summary>
        /// 添加角色同时关联组织
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[Authorize(IdentityPermissions.Roles.Create)]
        //[Authorize(TigerIdentityPermissions.Roles.AddOrganizationUnitRole)]
        public virtual async Task<IdentityRoleDto> CreateAsync(IdentityRoleOrgCreateDto input)
        {
            var role = await base.CreateAsync(
                ObjectMapper.Map<IdentityRoleOrgCreateDto, IdentityRoleCreateDto>(input)
            );
            if (input.OrgId.HasValue)
            {
                await OrgManager.AddRoleToOrganizationUnitAsync(role.Id, input.OrgId.Value);
            }
            return role;
        }
    }
}
