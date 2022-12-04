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

    [ApiExplorerSettings(GroupName = ApiExplorerConsts.IdentityServerGroupName)]
    [RemoteService(Name = IdentityServerClientConsts.RemoteServiceName)]
    [Area("identity-server")]
    [ControllerName("ApiResource")]
    [Route("/v1/identity-server/api-resource")]
    public class ApiResourceController : IApiResourceAppService
    {
        [HttpPost]
        public Task<ApiResourceDto> CreateAsync(CreateUpdateApiResourceDto input)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}")]
        public Task<ApiResourceDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public Task<PagedResultDto<ApiResourceDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{id}")]
        public Task<ApiResourceDto> UpdateAsync(Guid id, CreateUpdateApiResourceDto input)
        {
            throw new NotImplementedException();
        }
    }
}
