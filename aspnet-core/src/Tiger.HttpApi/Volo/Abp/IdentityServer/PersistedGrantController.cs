﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.IdentityServer.Grants;
using Tiger.Volo.Abp.IdentityServer.Grants.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Volo.Abp.IdentityServer
{   
    /// <summary>
    /// 持续授权
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.IdentityServerGroupName)]
    [RemoteService(Name = "PersistedGrant")]
    [Area("identity-server")]
    [ControllerName("IdentityResource")]
    [Route("/v1/identity-server/persisted-grant")]
    public class PersistedGrantController: AbpController, IPersistedGrantAppService
    {
        public PersistedGrantController(IPersistedGrantAppService persistedGrantAppService)
        {
            PersistedGrantAppService=persistedGrantAppService;
        }

        protected IPersistedGrantAppService PersistedGrantAppService { get; }

        public async Task<PersistedGrantDto> CreateAsync(CreateUpdatePersistedGrantDto input)
        {
            return await PersistedGrantAppService.CreateAsync(input);
        }

        public async Task DeleteAsync(Guid id)
        {
            await PersistedGrantAppService.DeleteAsync(id);
            return;
        }

        public async Task<PersistedGrantDto> GetAsync(Guid id)
        {
            return await PersistedGrantAppService.GetAsync(id);
        }

        public async Task<PagedResultDto<PersistedGrantDto>> GetListAsync(GetPersistedGrantInput input)
        {
            return await PersistedGrantAppService.GetListAsync(input);
        }

        public async Task<PersistedGrantDto> UpdateAsync(Guid id, CreateUpdatePersistedGrantDto input)
        {
            return await PersistedGrantAppService.UpdateAsync(id, input);
        }
    }
}
