﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.IdentityServer.Devices;
using Tiger.Volo.Abp.IdentityServer.IdentityResources;
using Tiger.Volo.Abp.IdentityServer.IdentityResources.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.Devices;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.Uow;

namespace Tiger.Volo.Abp.IdentityServer
{
    /// <summary>
    /// 身份资源
    /// </summary>
    [RemoteService(false)]
    [Authorize(IdentityServerPermissions.IdentityResources.Default)]
    public class IdentityResourceAppService :
        CrudAppService<
            IdentityResource,
            IdentityResourceDto,
            Guid, //Primary key 
            GetIdentityResourceDto, //Used for paging/sorting
            CreateUpdateIdentityResourceDto>, IIdentityResourceAppService
    {
        protected ITigerIdentityResourceRepository IdentityResourceRepository { get; }
        protected IPermissionChecker PermissionChecker { get; }

        public IdentityResourceAppService(
            IRepository<IdentityResource, Guid> repository,
            ITigerIdentityResourceRepository identityResourceRepository,
            IPermissionChecker permissionChecker) : base(repository)
        {
            IdentityResourceRepository=identityResourceRepository;
            PermissionChecker=permissionChecker;
        }

        /// <summary>
        /// 获取标识资源详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<IdentityResourceDto> GetAsync(Guid id)
        {
            var identityResource =  await IdentityResourceRepository.GetAsync(id);
            return ObjectMapper.Map<IdentityResource, IdentityResourceDto>(identityResource);
        }


        /// <summary>
        /// 获取标识资源列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  async Task<PagedResultDto<IdentityResourceDto>> GetListAsync(IdentityResourceGetByPagedDto input)
        {
            var identityResources =  await IdentityResourceRepository.GetListAsync(
                input.Sorting,
                input.SkipCount,
                input.MaxResultCount,
                input.Filter,
                true);
            // Todo: 获取总数量方法修改 需要把所有过滤条件加上调用两次有点麻烦
            var totalCount = await IdentityResourceRepository.GetCountAsync();
            
            return new PagedResultDto<IdentityResourceDto>(
                totalCount, 
                ObjectMapper.Map<List<IdentityResource>, List<IdentityResourceDto>>(identityResources));
        }

        /// <summary>
        /// 创建标识资源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public  async Task<IdentityResourceDto> CreateAsync(IdentityResourceCreateOrUpdateDto input)
        {
            var isExisit = await IdentityResourceRepository.CheckNameExistAsync(input.Name);
            if (isExisit)
            {
                throw new UserFriendlyException("标识资源已经存在");
            }

            var identityResource = new IdentityResource(
                GuidGenerator.Create(), 
                input.Name,
                input.DisplayName,
                input.Description,
                input.Enabled,
                input.Required,
                showInDiscoveryDocument:input.ShowInDiscoveryDocument);
            await IdentityResourceRepository.InsertAsync(identityResource);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<IdentityResource,IdentityResourceDto>(identityResource);
        }


        public override Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);    
        }

        public  async Task<IdentityResourceDto> UpdateAsync(Guid id, IdentityResourceCreateOrUpdateDto input)
        {   
            var identityResouce = await IdentityResourceRepository.GetAsync(id);

            await UpdateApiResourceByInputAsync(identityResouce, input);


            await IdentityResourceRepository.UpdateAsync(identityResouce);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<IdentityResource, IdentityResourceDto>(identityResouce);
        }


        /// <summary>
        /// 通过传入的数据修改标识资源对象
        /// </summary>
        /// <param name="identityResource"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected async virtual Task UpdateApiResourceByInputAsync(IdentityResource identityResource, IdentityResourceCreateOrUpdateDto input)
        {
            if (!string.Equals(identityResource.Name, input.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                identityResource.Name = input.Name;
            }
            if (!string.Equals(identityResource.Description, input.Description, StringComparison.InvariantCultureIgnoreCase))
            {
                identityResource.Description = input.Description;
            }
            if (!string.Equals(identityResource.DisplayName, input.DisplayName, StringComparison.InvariantCultureIgnoreCase))
            {
                identityResource.DisplayName = input.DisplayName;
            }
            identityResource.Emphasize = input.Emphasize;
            identityResource.Enabled = input.Enabled;
            identityResource.Required = input.Required;
            identityResource.ShowInDiscoveryDocument = input.ShowInDiscoveryDocument;

            /*
             关联表更新
             1. 移除前端没有传入的数据
             2. 新增前端传入但是数据库中没有的数据
             3. 修改数据中存在前端传入的数据
             
             */
            if (await PermissionChecker.IsGrantedAsync(IdentityServerPermissions.IdentityResources.ManageClaims))
            {
                // 删除不存在的UserClaim
                identityResource.UserClaims.RemoveAll(claim => input.UserClaims.Any(inputClaim => claim.Type == inputClaim.Type));
                foreach (var inputClaim in input.UserClaims)
                {
                    var userClaim = identityResource.FindUserClaim(inputClaim.Type);
                    if (userClaim == null)
                    {
                        identityResource.AddUserClaim(inputClaim.Type);
                    }
                }
            }

            #region Property
            if (await PermissionChecker.IsGrantedAsync(IdentityServerPermissions.IdentityResources.ManageProperties))
            {
                identityResource.Properties.RemoveAll(prop => !input.Properties.Any(inputProp => prop.Key == inputProp.Key));
                foreach (var inputProp in input.Properties)
                {
                    var identityResourceProperty = identityResource.Properties.FirstOrDefault( x => x.Key ==  inputProp.Key);
                    if (identityResourceProperty.Key == null)
                    {
                        identityResource.Properties.Add(inputProp.Key, inputProp.Value);
                    }
                    else
                    {   
                        identityResource.Properties.Remove(inputProp.Key);
                        identityResource.Properties.Add(inputProp.Key, inputProp.Value);
                    }
                }
            }
            #endregion
        }
    }
}
