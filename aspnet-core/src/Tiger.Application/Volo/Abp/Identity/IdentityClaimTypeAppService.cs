using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity.ClaimTypes;
using Tiger.Volo.Abp.Identity.ClaimTypes.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;

namespace Tiger.Volo.Abp.Identity
{
    /// <summary>
    /// 用户声明类型
    /// </summary>
    [RemoteService(false)]
    [Authorize(TigerIdentityPermissions.ClaimTypes.Default)]
    public class IdentityClaimTypeAppService : IdentityAppServiceBase, IIdentityClaimTypeAppService
    {
        protected IIdentityClaimTypeRepository IdentityClaimTypeRepository { get; }

        //TODO: Rename to IdentityClaimTypeManager in v2.0!
        protected IdenityClaimTypeManager IdentityClaimTypeManager { get; }

        public IdentityClaimTypeAppService(IIdentityClaimTypeRepository identityClaimTypeRepository, IdenityClaimTypeManager idenityClaimTypeManager)
        {
            IdentityClaimTypeRepository=identityClaimTypeRepository;
            IdentityClaimTypeManager=idenityClaimTypeManager;
        }

        
        /// <summary>
        /// 检查声明类型
        /// </summary>
        /// <param name="claimType"></param>
        /// <exception cref="BusinessException"></exception>
        protected virtual void CheckChangingClaimType(IdentityClaimType claimType)
        {
            if (claimType.IsStatic)
            {
                throw new BusinessException(IdentityErrorCodes.StaticClaimTypeChange);
            }
        }

        /// <summary>
        /// 创建声明类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        [Authorize(TigerIdentityPermissions.ClaimTypes.Create)]
        public async Task<ClaimTypeDto> CreateAsync(CreateClaimTypeDto input)
        {
            var isExisit =  await IdentityClaimTypeRepository.AnyAsync(input.Name);
            if (isExisit)
            {
                throw new UserFriendlyException(L["IdentityClaimTypeAlreadyExists"], input.Name);
            }

            var identityClaimType = new IdentityClaimType(
                GuidGenerator.Create(),
                input.Name,
                input.Required,
                input.Required,
                input.Regex,
                input.RegexDescription,
                input.Description,
                input.ValueType);
            await IdentityClaimTypeManager.CreateAsync(identityClaimType);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<IdentityClaimType, ClaimTypeDto>(identityClaimType);
        }

        [Authorize(TigerIdentityPermissions.ClaimTypes.Delete)]
        public async Task DeleteAsync(Guid id)
        {   
            // Find 如果查询不到返回null
            var identityClaimType = await IdentityClaimTypeRepository.FindAsync(id);

            if (identityClaimType == null)
            {   
                // Task类型 直接return
                return;
            }

            if (identityClaimType.IsStatic)
            {   
                //先定义好需要抛出的错误异常 常量
                throw new BusinessException(IdentityErrorCodes.StaticClaimTypeDeletion);
            }

            await IdentityClaimTypeRepository.DeleteAsync(identityClaimType);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 获取所有声明类型
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 有些没有必要的注释可以省略
        /// </remarks>
        [Authorize(TigerIdentityPermissions.ClaimTypes.Default)]
        public async Task<List<ClaimTypeDto>> GetAllListAsync()
        {   
            // 基类有获取所有的方法
            var identityClaimTypes = await IdentityClaimTypeRepository.GetListAsync();

            return ObjectMapper.Map<List<IdentityClaimType>, List<ClaimTypeDto>>(identityClaimTypes);

        }

        [Authorize(TigerIdentityPermissions.ClaimTypes.Default)]
        public async Task<ClaimTypeDto> GetAsync(Guid id)
        {
            var identityClaimType = await IdentityClaimTypeRepository.GetAsync(id);

            return ObjectMapper.Map<IdentityClaimType, ClaimTypeDto>(identityClaimType);
            throw new NotImplementedException();
        }

        [Authorize(TigerIdentityPermissions.ClaimTypes.Default)]
        public async Task<PagedResultDto<ClaimTypeDto>> GetListAsync(GetIdentityClaimTypesInput input)
        {
            var claimTypeCount = await IdentityClaimTypeRepository.GetCountAsync(input.Filter);

            var claimTypes = await IdentityClaimTypeRepository.GetListAsync(
                input.Sorting,input.MaxResultCount,input.SkipCount,input.Filter);
            return new PagedResultDto<ClaimTypeDto>(claimTypeCount,
                ObjectMapper.Map<List<IdentityClaimType>, List<ClaimTypeDto>>(claimTypes));

        }

        [Authorize(TigerIdentityPermissions.ClaimTypes.Update)]
        public async Task<ClaimTypeDto> UpdateAsync(Guid id, UpdateClaimTypeDto input)
        {
            // Update 如果属性很多 只能一个一个赋值？
            var identityClaimType = await IdentityClaimTypeRepository.GetAsync(id);
            CheckChangingClaimType(identityClaimType);

            identityClaimType.Required = input.Required;

            if (!string.Equals(identityClaimType.Regex, input.Regex, StringComparison.InvariantCultureIgnoreCase))
            {
                identityClaimType.Regex = input.Regex;
            }

            identityClaimType.RegexDescription = input.RegexDescription;

            identityClaimType.Description = input.Description;

            identityClaimType = await IdentityClaimTypeManager.UpdateAsync(identityClaimType);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<IdentityClaimType, ClaimTypeDto>(identityClaimType);

        }
    }
}
