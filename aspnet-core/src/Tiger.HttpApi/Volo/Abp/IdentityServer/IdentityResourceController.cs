using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Volo.Abp.IdentityServer
{

    /// <summary>
    /// 标识资源
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.IdentityServerGroupName)]
    [RemoteService(Name = "IdentityServerResource")]
    [Area("identity-server")]
    [ControllerName("IdentityResource")]
    [Route("api/identity-server/identity-resources")]
    public class IdentityResourceController : AbpController, IIdentityResourceAppService
    {   
        protected IIdentityResourceAppService _identityResourceAppService;

        public IdentityResourceController(IIdentityResourceAppService identityResourceAppService)
        {
            _identityResourceAppService=identityResourceAppService;
        }


        /// <summary>
        /// 创建标识资源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        public async Task<IdentityResourceDto> CreateAsync(IdentityResourceCreateOrUpdateDto input)
        {
            return await _identityResourceAppService.CreateAsync(input);
        }


        /// <summary>
        /// 删除标识资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _identityResourceAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取标识资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("{id}")]
        public async Task<IdentityResourceDto> GetAsync(Guid id)
        {
            return await _identityResourceAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取标识资源列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<IdentityResourceDto>> GetListAsync(IdentityResourceGetByPagedDto input)
        {
            return await _identityResourceAppService.GetListAsync(input);
        }


        /// <summary>
        /// 更新标识资源
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IdentityResourceDto> UpdateAsync(Guid id, IdentityResourceCreateOrUpdateDto input)
        {
            return await _identityResourceAppService.UpdateAsync(id, input);
        }

    }
}
