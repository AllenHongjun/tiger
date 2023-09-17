using System;
using System.Threading.Tasks;
using Tiger.Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.IdentityServer.ApiResources;

namespace Tiger.Volo.Abp.IdentityServer
{
    /// <summary>
    /// Api作用域
    /// </summary>
    /// <remarks>
    /// </remarks>
    [RemoteService(false)]
    public class ApiScopeAppService : ApplicationService, IApiScopeAppService
    {
        public ApiScopeAppService(IApiResourceRepository apiResourceRepository)
        {
            ApiResourceRepository=apiResourceRepository;
        }
        

        protected IApiResourceRepository ApiResourceRepository { get; }

        public Task<ApiScopeDto> CreateAsync(ApiScopeCreateDto input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiScopeDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<ApiScopeDto>> GetListAsync(GetApiScopeInput input)
        {
            throw new NotImplementedException();
        }

        public Task<ApiScopeDto> UpdateAsync(Guid id, ApiScopeUpdateDto input)
        {
            throw new NotImplementedException();
        }
    }
}
