using Microsoft.AspNetCore.Mvc;
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
    [ControllerName("PersistedGrant")]
    [Route("api/identity-server/persisted-grants")]
    public class PersistedGrantController: AbpController, IPersistedGrantAppService
    {
        public PersistedGrantController(IPersistedGrantAppService persistedGrantAppService)
        {
            PersistedGrantAppService=persistedGrantAppService;
        }

        protected IPersistedGrantAppService PersistedGrantAppService { get; }


        /// <summary>
        /// 创建授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PersistedGrantDto> CreateAsync(CreateUpdatePersistedGrantDto input)
        {
            return await PersistedGrantAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新授权
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<PersistedGrantDto> UpdateAsync(Guid id, CreateUpdatePersistedGrantDto input)
        {
            return await PersistedGrantAppService.UpdateAsync(id, input);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await PersistedGrantAppService.DeleteAsync(id);
            return;
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<PersistedGrantDto> GetAsync(Guid id)
        {
            return await PersistedGrantAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<PersistedGrantDto>> GetListAsync(GetPersistedGrantInput input)
        {
            return await PersistedGrantAppService.GetListAsync(input);
        }

        
    }
}
