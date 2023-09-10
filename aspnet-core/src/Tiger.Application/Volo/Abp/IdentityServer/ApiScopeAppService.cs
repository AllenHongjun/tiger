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

        // TODO:  实现 apiScope的仓储
        //protected IApiScopeRepository ApiScopeRepository { get; }

        protected IApiResourceRepository ApiResourceRepository { get; }

        public async Task<ApiScopeDto> CreateAsync(ApiScopeCreateDto input)
        {
            //if (await ApiScopeRepository.CheckNameExistAsync(input.Name))
            //{
            //    throw new UserFriendlyException(L[AbpIdentityServerErrorConsts.ApiScopeNameExisted, input.Name]);
            //}
            //var apiScope = new ApiScope(
            //    GuidGenerator.Create(),
            //    input.Name,
            //    input.DisplayName,
            //    input.Description,
            //    input.Required,
            //    input.Emphasize,
            //    input.ShowInDiscoveryDocument);

            //await UpdateApiScopeByInputAsync(apiScope, input);

            //await CurrentUnitOfWork.SaveChangesAsync();

            //apiScope = await ApiScopeRepository.InsertAsync(apiScope);

            //return ObjectMapper.Map<ApiScope, ApiScopeDto>(apiScope);

            throw new NotImplementedException();
        }


        // api scorpe de claim 单独一个接口   
        //if (await IsGrantAsync(IdentityServerPermissions.Clients.ManageProperties))
        //    {
        //        // 移除不存在的属性
        //        client.Properties.RemoveAll(prop => !input.Properties.Any(inputProp => prop.Key == inputProp.Key));
        //        foreach (var inputProp in input.Properties)
        //        {
        //            var findProp = client.Properties.Find(x => x.Key == inputProp.Key);
        //            if (findProp == null)
        //            {
        //                client.AddProperty(inputProp.Key, inputProp.Value);
        //            }
        //            else
        //            {
        //                findProp.Value = inputProp.Value;
        //            }
        //        }
        //    }

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
