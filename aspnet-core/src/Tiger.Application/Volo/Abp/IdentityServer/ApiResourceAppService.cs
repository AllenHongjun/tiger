
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Books;
using Tiger.Module.System.Localization;
using Tiger.Volo.Abp.IdentityServer.ApiResources;
using Tiger.Volo.Abp.IdentityServer.ApiResources.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.IdentityServer.ApiResources;

namespace Tiger.Volo.Abp.IdentityServer
{
    /// <summary>
    /// Api资源
    /// </summary>
    [RemoteService(false)]
    [Authorize(IdentityServerPermissions.ApiResources.Default)]
    public class ApiResourceAppService :
        AbpIdentityServerAppServiceBase, IApiResourceAppService
    {
        protected IApiResourceRepository ApiResourceRepository { get; }
        protected ITigerApiResourceRepository TigerApiResourceRepository { get; }
        protected new IPermissionChecker PermissionChecker { get; }

        public ApiResourceAppService(
            IApiResourceRepository apiResourceRepository,
            ITigerApiResourceRepository tigerApiResourceRepository,
            IPermissionChecker permissionChecker) : base(permissionChecker)
        {
            ApiResourceRepository=apiResourceRepository;
            TigerApiResourceRepository=tigerApiResourceRepository;
            PermissionChecker=permissionChecker;
        }

        public async virtual Task<ApiResourceDto> GetAsync(Guid id)
        {
            var apiResource = await ApiResourceRepository.GetAsync(id);

            return ObjectMapper.Map<ApiResource, ApiResourceDto>(apiResource);
        }

        /// <summary>
        /// 分页查询Api资源列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async virtual Task<PagedResultDto<ApiResourceDto>> GetListAsync(ApiResourceGetByPagedInputDto input)
        {
            var apiResources = await ApiResourceRepository.GetListAsync(input.Sorting,
                input.SkipCount, input.MaxResultCount,
                input.Filter);

            // 重载GetCount方法，增加filter参数
            var apiResourceCount = await TigerApiResourceRepository.GetCountAsync(input.Filter);

            return new PagedResultDto<ApiResourceDto>(apiResourceCount,
                ObjectMapper.Map<List<ApiResource>, List<ApiResourceDto>>(apiResources));
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        [Authorize(IdentityServerPermissions.ApiResources.Create)]
        public async virtual Task<ApiResourceDto> CreateAsync(ApiResourceCreateDto input)
        {
            var apiResourceExists = await ApiResourceRepository.CheckNameExistAsync(input.Name);
            if (apiResourceExists)
            {
                throw new UserFriendlyException(L[AbpIdentityServerErrorConsts.ApiResourceNameExisted, input.Name]);
            }
            var apiResource = new ApiResource(
                GuidGenerator.Create(),
                input.Name,
                input.DisplayName,
                input.Description);

            await UpdateApiResourceByInputAsync(apiResource, input);

            apiResource = await ApiResourceRepository.InsertAsync(apiResource);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<ApiResource, ApiResourceDto>(apiResource);
        }

        /// <summary>
        /// 更新Api资源
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(IdentityServerPermissions.ApiResources.Update)]
        public async virtual Task<ApiResourceDto> UpdateAsync(Guid id, ApiResourceUpdateDto input)
        {
            var apiResource = await ApiResourceRepository.GetAsync(id);

            await UpdateApiResourceByInputAsync(apiResource, input);

            apiResource = await ApiResourceRepository.UpdateAsync(apiResource);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<ApiResource, ApiResourceDto>(apiResource);
        }

        /// <summary>
        /// 删除Api资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(IdentityServerPermissions.ApiResources.Delete)]
        public async virtual Task DeleteAsync(Guid id)
        {
            var apiResource = await ApiResourceRepository.GetAsync(id);
            await ApiResourceRepository.DeleteAsync(apiResource);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        protected async virtual Task UpdateApiResourceByInputAsync(ApiResource apiResource, ApiResourceCreateOrUpdateDto input)
        {
            //apiResource.ShowInDiscoveryDocument = input.ShowInDiscoveryDocument;
            apiResource.Enabled = input.Enabled;

            //if (!string.Equals(apiResource.AllowedAccessTokenSigningAlgorithms, input.AllowedAccessTokenSigningAlgorithms, StringComparison.InvariantCultureIgnoreCase))
            //{
            //    apiResource.AllowedAccessTokenSigningAlgorithms = input.AllowedAccessTokenSigningAlgorithms;
            //}
            if (!string.Equals(apiResource.DisplayName, input.DisplayName, StringComparison.InvariantCultureIgnoreCase))
            {
                apiResource.DisplayName = input.DisplayName;
            }
            if (apiResource.Description?.Equals(input.Description, StringComparison.InvariantCultureIgnoreCase)
                == false)
            {
                apiResource.Description = input.Description;
            }

            if (await IsGrantAsync(IdentityServerPermissions.ApiResources.ManageClaims))
            {
                // 删除不存在的UserClaim
                apiResource.UserClaims.RemoveAll(claim => !input.UserClaims.Any(inputClaim => claim.Type == inputClaim.Type));
                foreach (var inputClaim in input.UserClaims)
                {
                    var userClaim = apiResource.FindClaim(inputClaim.Type);
                    if (userClaim == null)
                    {
                        apiResource.AddUserClaim(inputClaim.Type);
                    }
                }
            }

            if (await IsGrantAsync(IdentityServerPermissions.ApiResources.ManageScopes))
            {
                // 除了前端传递过来scope之外，全部删除，（先删除不要scope,再将没有的scope添加上）
                apiResource.Scopes.RemoveAll(scope => !input.Scopes.Any(inputScope => scope.Name == inputScope.Scope));
                foreach (var inputScope in input.Scopes)
                {
                    var scope = apiResource.FindScope(inputScope.Scope);
                    if (scope == null)
                    {
                        apiResource.AddScope(inputScope.Scope);
                    }
                }
            }

            if (await IsGrantAsync(IdentityServerPermissions.ApiResources.ManageSecrets))
            {
                // 删除不存在的Secret
                apiResource.Secrets.RemoveAll(secret => !input.Secrets.Any(inputSecret => secret.Type == inputSecret.Type && secret.Value == inputSecret.Value));
                foreach (var inputSecret in input.Secrets)
                {
                    // 第一次重复校验已经加密过的字符串
                    if (apiResource.FindSecret(inputSecret.Value, inputSecret.Type) == null)
                    {
                        var apiSecretValue = inputSecret.Value;
                        if (IdentityServerConstants.SecretTypes.SharedSecret.Equals(inputSecret.Type))
                        {
                            if (inputSecret.HashType == HashType.Sha256)
                            {
                                apiSecretValue = inputSecret.Value.Sha256();
                            }
                            else if (inputSecret.HashType == HashType.Sha512)
                            {
                                apiSecretValue = inputSecret.Value.Sha512();
                            }
                        }
                        // 加密之后还需要做一次校验 避免出现重复值
                        var secret = apiResource.FindSecret(apiSecretValue, inputSecret.Type);
                        if (secret == null)
                        {
                            apiResource.AddSecret(apiSecretValue, inputSecret.Expiration, inputSecret.Type, inputSecret.Description);
                        }
                    }
                }
            }

            // TODO: 增加 FindProperty AddProperty方法管理 Properties
            //if (await IsGrantAsync(IdentityServerPermissions.ApiResources.ManageProperties))
            //{
            //    // 删除不存在的属性
            //    apiResource.Properties.RemoveAll(prop => !input.Properties.Any(inputProp => prop.Key == inputProp.Key));
            //    foreach (var inputProp in input.Properties)
            //    {
            //        var apiResourceProperty = apiResource.FindProperty(inputProp.Key);
            //        if (apiResourceProperty == null)
            //        {
            //            apiResource.AddProperty(inputProp.Key, inputProp.Value);
            //        }
            //        else
            //        {
            //            apiResourceProperty.Value = inputProp.Value;
            //        }
            //    }
            //}
        }


    }
}
