using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tiger.Volo.Abp.IdentityServer.ApiResources;
using Tiger.Volo.Abp.IdentityServer.ApiResources.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Volo.Abp.IdentityServer
{

    /// <summary>
    /// Api资源
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.IdentityServerGroupName)]
    [RemoteService(Name = IdentityServerClientConsts.RemoteServiceName)]
    [Area("identity-server")]
    [ControllerName("ApiResource")]
    [Route("api/identity-server/api-resources")]
    public class ApiResourceController : AbpController, IApiResourceAppService
    {
        public ApiResourceController(IApiResourceAppService apiResourceAppService)
        {
            ApiResourceAppService=apiResourceAppService;
        }

        protected IApiResourceAppService ApiResourceAppService { get; }


        [HttpPost]
        public async Task<ApiResourceDto> CreateAsync(ApiResourceCreateDto input)
        {
            return await ApiResourceAppService.CreateAsync(input);
        }

        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await ApiResourceAppService.DeleteAsync(id);
            return;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ApiResourceDto> GetAsync(Guid id)
        {
            return await ApiResourceAppService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<ApiResourceDto>> GetListAsync(ApiResourceGetByPagedInputDto input)
        {
            return await ApiResourceAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ApiResourceDto> UpdateAsync(Guid id, ApiResourceUpdateDto input)
        {
            return await ApiResourceAppService.UpdateAsync(id, input);
        }
    }
}
