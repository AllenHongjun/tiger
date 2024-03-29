using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.System.Localization.Dtos;
using Volo.Abp.Application.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using Tiger.Module.System.Localization.Permissions;
using Volo.Abp.SettingManagement;
using Tiger.Module.System.Platform.Layouts.Dto;

namespace Tiger.Module.System.Localization;


/// <summary>
/// 语言管理服务
/// </summary>
[RemoteService(IsEnabled = false)]
[Authorize(LocalizationPermissions.Languages.Default)]
public class LanguageAppService : ApplicationService, ILanguageAppService
{


    protected ILanguageRepository LanguageRepository { get; }
    protected ISettingManager SettingManager { get; }

    public LanguageAppService(ILanguageRepository repository, ISettingManager settingManager)
    {
        LanguageRepository = repository;
        SettingManager=settingManager;
    }

    #region 语言管理


    public async void GetCultureList()
    {

    }

    /// <summary>
    /// 分页查询语言
    /// </summary>     
    public async Task<PagedResultDto<LanguageDto>> GetListAsync(LanguageGetListInput input)
    {
        var totalCount = await LanguageRepository.CountAsync(input.Filter);
        var languages = await LanguageRepository.GetListAsync(input.MaxResultCount, input.SkipCount, input.Filter);
        return new PagedResultDto<LanguageDto>(totalCount, ObjectMapper.Map<List<Language>, List<LanguageDto>>(languages));
    }

    public async Task<LanguageDto> GetAsync(Guid id)
    {
        var language = await LanguageRepository.GetAsync(id);
        return ObjectMapper.Map<Language, LanguageDto>(language);
    }

    /// <summary>
    /// 创建语言
    /// </summary>
    [Authorize(LocalizationPermissions.Languages.Create)]
    public virtual async Task<LanguageDto> CreateAsync(CreateLanguageDto input)
    {
        var language = await LanguageRepository.FindAsync(x => x.CultureName == input.CultureName);
        if (language != null)
        {
            throw new BusinessException(LocalizationErrorCodes.LanguageExist);
        }

        language = new Language(
            GuidGenerator.Create(),
            input.Enable,
            input.CultureName,
            input.UiCultureName,
            input.DisplayName,
            input.FlagIcon);

        language = await LanguageRepository.InsertAsync(language);

        return ObjectMapper.Map<Language, LanguageDto>(language);
    }

    /// <summary>
    /// 编辑语言
    /// </summary>
    [Authorize(LocalizationPermissions.Languages.Update)]
    public async Task<LanguageDto> UpdateAsync(Guid id, UpdateLanguageDto input)
    {
        var language = await LanguageRepository.GetAsync(id);
        if (language == null)
        {
            throw new BusinessException(LocalizationErrorCodes.LanguageNotFound);
        }
        language.DisplayName = input.DisplayName;
        language.Enable = input.Enable;
        language = await LanguageRepository.UpdateAsync(language);

        await CurrentUnitOfWork.SaveChangesAsync();
        return ObjectMapper.Map<Language, LanguageDto>(language);
    }

    /// <summary>W
    /// 删除语言
    /// </summary>
    [Authorize(LocalizationPermissions.Languages.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        var language = await LanguageRepository.GetAsync(id);
        await LanguageRepository.DeleteAsync(language);
        await CurrentUnitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// 设置默认语言
    /// </summary>
    [Authorize(LocalizationPermissions.Languages.ChangeDefault)]
    public async Task SetDefaultAsync(Guid id)
    {
        var language = await LanguageRepository.FindAsync(id);

        // 全局设置当前租户的默认语言(更新系统设置表数据)
        await SettingManager.SetForCurrentTenantAsync(LocalizationConsts.SettingDefaultLanguage, language.CultureName + ";" + language.UiCultureName, false);

        if (language == null)
        {
            throw new BusinessException(LocalizationErrorCodes.LanguageNotFound);
        }

        // 更新数据中默认语言设置
        var defaultLanguge = await LanguageRepository.FindDefaultLanguageAsync();
        if (defaultLanguge != null && language.Id == defaultLanguge.Id) return;

        if (defaultLanguge != null)
        {
            defaultLanguge.SetDefault(false);
            await LanguageRepository.UpdateAsync(defaultLanguge);
        }
        
        language.SetDefault(true);
        await LanguageRepository.UpdateAsync(language);
        await CurrentUnitOfWork.SaveChangesAsync();
    } 
    #endregion

}
