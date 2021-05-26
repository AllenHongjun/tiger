using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace TigerAdmin.Volo.Abp.Identity
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
        public TigerIdentityUserController(ITigerIdentityUserAppService userAppService)
        {
            UserAppService = userAppService;
        }

        protected ITigerIdentityUserAppService UserAppService { get; }


        [HttpPost]
        [Route("{userId}/add-to-organizations")]
        public Task AddToOrganizationUnitsAsync(Guid userId, List<Guid> ouIds)
        {
            return UserAppService.AddToOrganizationUnitsAsync(userId, ouIds);
        }

        [HttpPost]
        [Route("create-to-organizations")]
        public Task<IdentityUserDto> CreateAsync(IdentityUserOrgCreateDto input)
        {
            return UserAppService.CreateAsync(input);
        }


        [HttpGet]
        [Route("{id}/organizations")]
        public Task<ListResultDto<OrganizationUnitDto>> GetListOrganizationUnitsAsync(Guid id, bool includeDetails = false)
        {
            return UserAppService.GetListOrganizationUnitsAsync(id, includeDetails);
        }

        [HttpPut]
        [Route("{id}/update-to-organizations")]
        public Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserOrgUpdateDto input)
        {
            return UserAppService.UpdateAsync(id, input);
        }
    }
}
