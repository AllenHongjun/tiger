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

namespace Tiger.Volo.Abp.IdentityServer
{

    /// <summary>
    /// Api资源
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.IdentityServerGroupName)]
    [RemoteService(Name = IdentityServerClientConsts.RemoteServiceName)]
    [Area("identity-server")]
    [ControllerName("ApiResource")]
    [Route("/v1/identity-server/api-resource")]
    public class ApiResourceController : IApiResourceAppService
    {
        public ApiResourceController(IApiResourceAppService apiResourceAppService)
        {
            ApiResourceAppService=apiResourceAppService;
        }

        protected IApiResourceAppService ApiResourceAppService { get; }


        [HttpPost]
        public async Task<ApiResourceDto> CreateAsync(CreateUpdateApiResourceDto input)
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
        public async Task<PagedResultDto<ApiResourceDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return await ApiResourceAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ApiResourceDto> UpdateAsync(Guid id, CreateUpdateApiResourceDto input)
        {
            return await ApiResourceAppService.UpdateAsync(id, input);
        }
    }
}
