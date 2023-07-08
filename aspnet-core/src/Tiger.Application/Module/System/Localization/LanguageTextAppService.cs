using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.System.Localization.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using System.Collections.Generic;
using Volo.Abp.ObjectMapping;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Tiger.Module.System.Localization.Permissions;
using Volo.Abp.Uow;

namespace Tiger.Module.System.Localization;


/// <summary>
/// ”Ô—‘Œƒ±æ
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

    [Authorize(LocalizationPermissions.LanguageTexts.Create)]
    public async Task<LanguageTextDto> CreateAsync(CreateLanguageTextDto input)
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
        return ObjectMapper.Map<LanguageText,LanguageTextDto>(languageText);
    }

    [Authorize(LocalizationPermissions.LanguageTexts.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        var languageText = await LanguageTextRepository.GetAsync(id);
        await LanguageTextRepository.DeleteAsync(languageText);
    }

    public async Task<PagedResultDto<LanguageTextDto>> GetListAsync(LanguageTextGetListInput input)
    {
        var totalCount = await LanguageTextRepository.GetCountAsync();
        var list = await LanguageTextRepository.GetListAsync(input.CultureName, input.ResourceName, input.Filter, input.MaxResultCount, input.SkipCount);
        return  new PagedResultDto<LanguageTextDto>(totalCount,ObjectMapper.Map<List<LanguageText>,List<LanguageTextDto>>(list));
    }

    [Authorize(LocalizationPermissions.LanguageTexts.Edit)]
    public Task<LanguageTextDto> UpdateAsync(Guid id, UpdateLanguageTextDto input)
    {
        throw new NotImplementedException();
    }
    
}
