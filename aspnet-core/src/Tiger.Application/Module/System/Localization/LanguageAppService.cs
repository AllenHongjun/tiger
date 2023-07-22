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

namespace Tiger.Module.System.Localization;


/// <summary>
/// ���Թ������
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



    /// <summary>
    /// ��ҳ��ѯ����
    /// </summary>     
    public async Task<PagedResultDto<LanguageTextDto>> GetListAsync(LanguageGetListInput input)
    {
        var totalCount = await LanguageRepository.CountAsync(input.Filter);
        var languages = await LanguageRepository.GetListAsync(input.MaxResultCount, input.SkipCount, input.Filter);
        return new PagedResultDto<LanguageTextDto>(totalCount, ObjectMapper.Map<List<Language>, List<LanguageTextDto>>(languages));
    }

    /// <summary>
    /// ��������
    /// </summary>
    [Authorize(LocalizationPermissions.Languages.Create)]
    public virtual async Task<LanguageTextDto> CreateAsync(LanguageTextDto input)
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

        return ObjectMapper.Map<Language, LanguageTextDto>(language);
    }

    /// <summary>
    /// �༭����
    /// </summary>
    [Authorize(LocalizationPermissions.Languages.Update)]
    public async Task<LanguageTextDto> UpdateAsync(Guid id, UpdateLanguageDto input)
    {
        var language = await LanguageRepository.GetAsync(id);
        if (language == null)
        {
            throw new BusinessException(LocalizationErrorCodes.LanguageNotFound);
        }
        language = await LanguageRepository.UpdateAsync(language);

        await CurrentUnitOfWork.SaveChangesAsync();
        return ObjectMapper.Map<Language, LanguageTextDto>(language);
    }

    /// <summary>W
    /// ɾ������
    /// </summary>
    [Authorize(LocalizationPermissions.Languages.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        var language = await LanguageRepository.GetAsync(id);
        await LanguageRepository.DeleteAsync(language);
        await CurrentUnitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// ����Ĭ������
    /// </summary>
    [Authorize(LocalizationPermissions.Languages.ChangeDefault)]
    public async Task SetDefaultAsync(Guid id)
    {
        var language = await LanguageRepository.FindAsync(id);

        // ȫ�����õ�ǰ�⻧��Ĭ������(����ϵͳ���ñ�����)
        await SettingManager.SetForCurrentTenantAsync(LocalizationConsts.SettingDefaultLanguage, language.CultureName + ";" + language.UiCultureName, false);

        if (language == null)
        {
            throw new BusinessException(LocalizationErrorCodes.LanguageNotFound);
        }

        // ����������Ĭ����������
        var defaultLanguge = await LanguageRepository.FindDefaultLanguageAsync();
        if (defaultLanguge != null && language.Id == defaultLanguge.Id) return;

        defaultLanguge.SetDefault(false);
        language.SetDefault(true);
        await LanguageRepository.UpdateAsync(defaultLanguge);
        await LanguageRepository.UpdateAsync(language);
        await CurrentUnitOfWork.SaveChangesAsync();
    }

}
