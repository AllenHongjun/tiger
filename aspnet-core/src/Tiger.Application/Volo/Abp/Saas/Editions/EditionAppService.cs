using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;
using Tiger.Volo.Abp.Sass.Permissions;
using Volo.Abp;
using Tiger.Volo.Abp.Sass.Tenants;

namespace Tiger.Volo.Abp.Sass.Editions;

/// <summary>
/// 版本服务
/// </summary>
[Authorize(AbpSaasPermissions.Editions.Default)]
[RemoteService(IsEnabled = false)]
public class EditionAppService : AbpSaasAppServiceBase, IEditionAppService
{
    protected EditionManager EditionManager { get; }
    protected IEditionRepository EditionRepository { get; }

    protected ITenantRepository TenantRepository { get; }

    public EditionAppService(
        EditionManager editionManager,
        IEditionRepository editionRepository,
        ITenantRepository tenantRepository)
    {
        EditionManager = editionManager;
        EditionRepository = editionRepository;
        TenantRepository=tenantRepository;
    }

    #region 版本
    [Authorize(AbpSaasPermissions.Editions.Create)]
    public async virtual Task<EditionDto> CreateAsync(EditionCreateDto input)
    {
        var edition = await EditionManager.CreateAsync(input.DisplayName);
        input.MapExtraPropertiesTo(edition);

        await EditionRepository.InsertAsync(edition);

        await CurrentUnitOfWork.SaveChangesAsync();

        return ObjectMapper.Map<Edition, EditionDto>(edition);
    }

    [Authorize(AbpSaasPermissions.Editions.Delete)]
    public async virtual Task DeleteAsync(Guid id)
    {
        var edition = await EditionRepository.GetAsync(id);

        await EditionManager.DeleteAsync(edition);
    }

    public async virtual Task<EditionDto> GetAsync(Guid id)
    {
        var edition = await EditionRepository.GetAsync(id, false);

        return ObjectMapper.Map<Edition, EditionDto>(edition);
    }

    public async virtual Task<PagedResultDto<EditionDto>> GetListAsync(EditionGetListInput input)
    {
        var totalCount = await EditionRepository.GetCountAsync(input.Filter);
        var editions = await EditionRepository.GetListAsync(
            input.Sorting,
            input.MaxResultCount,
            input.SkipCount,
            input.Filter
        );
        var editionList = ObjectMapper.Map<List<Edition>, List<EditionDto>>(editions);
        
        #region 关联每个版本的租户数量
        var editionTenantCountDic = await TenantRepository.GetEditionTenantCount();
        foreach (var edition in editionList)
        {
            edition.TenantCount = editionTenantCountDic.GetOrDefault(edition.Id);
        } 
        #endregion

        return new PagedResultDto<EditionDto>(
            totalCount, editionList
        );
    }

    [Authorize(AbpSaasPermissions.Editions.Update)]
    public async virtual Task<EditionDto> UpdateAsync(Guid id, EditionUpdateDto input)
    {
        var edition = await EditionRepository.GetAsync(id, false);

        if (!string.Equals(edition.DisplayName, input.DisplayName))
        {
            await EditionManager.ChangeDisplayNameAsync(edition, input.DisplayName);
        }
        //edition.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        input.MapExtraPropertiesTo(edition);

        await EditionRepository.UpdateAsync(edition);

        await CurrentUnitOfWork.SaveChangesAsync();

        return ObjectMapper.Map<Edition, EditionDto>(edition);
    }


    /// <summary>
    /// 移动所有租户到指定版本
    /// </summary>
    /// <param name="editionId">当前版本</param>
    /// <param name="dstEditionId">目标版本</param>
    /// <returns></returns>
    [Authorize(AbpSaasPermissions.Editions.Update)]
    public async virtual Task MoveAllTenantAsync(Guid editionId, Guid? dstEditionId)
    {
        var tenants =  await TenantRepository.GetListAsync(editionId: editionId);
        foreach (var tenant in tenants)
        {   
            if(dstEditionId == null)
            {
                tenant.EditionId = null;
            }
            else
            {
                tenant.EditionId = dstEditionId;
            }
            await TenantRepository.UpdateAsync(tenant);
        }
        await CurrentUnitOfWork.SaveChangesAsync();
    }
    #endregion
}
