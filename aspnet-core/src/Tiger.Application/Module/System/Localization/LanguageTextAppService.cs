using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiger.Module.System.Localization.Dtos;
using Tiger.Module.System.Localization.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.System.Localization;


/// <summary>
/// 语言文本
/// </summary>
[RemoteService(IsEnabled = false)]
[Authorize(LocalizationPermissions.LanguageTexts.Default)]
public class LanguageTextAppService : ApplicationService,ILanguageTextAppService
{

    protected  ILanguageTextRepository LanguageTextRepository { get; }

    public LanguageTextAppService(ILanguageTextRepository languageTextRepository) 
    {
        LanguageTextRepository = languageTextRepository;
    }

    #region 语言文本管理
    [Authorize(LocalizationPermissions.LanguageTexts.Create)]
    public async Task<LanguageDto> CreateAsync(CreateLanguageTextDto input)
    {
        var languageText = new LanguageText(
            GuidGenerator.Create(),
            input.CultureName,
            input.Key,
            input.Value,
            input.Value,
            input.ResourceName);

        languageText = await LanguageTextRepository.InsertAsync(languageText);
        await CurrentUnitOfWork.SaveChangesAsync();
        return ObjectMapper.Map<LanguageText, LanguageDto>(languageText);
    }

    [Authorize(LocalizationPermissions.LanguageTexts.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        var languageText = await LanguageTextRepository.GetAsync(id);
        await LanguageTextRepository.DeleteAsync(languageText);
    }

    public async Task<PagedResultDto<LanguageDto>> GetListAsync(LanguageTextGetListInput input)
    {
        var totalCount = await LanguageTextRepository.GetCountAsync();
        var list = await LanguageTextRepository.GetListAsync(input.CultureName, input.ResourceName, input.Filter, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<LanguageDto>(totalCount, ObjectMapper.Map<List<LanguageText>, List<LanguageDto>>(list));
    }

    [Authorize(LocalizationPermissions.LanguageTexts.Update)]
    public Task<LanguageDto> UpdateAsync(Guid id, UpdateLanguageTextDto input)
    {
        throw new NotImplementedException();
    } 
    #endregion

}
