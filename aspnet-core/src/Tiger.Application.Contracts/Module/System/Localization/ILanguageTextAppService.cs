using System;
using System.Threading.Tasks;
using Tiger.Module.System.Localization.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.System.Localization;

public interface ILanguageTextAppService :IApplicationService
{
    Task<LanguageDto> CreateAsync(CreateLanguageTextDto input);
    Task DeleteAsync(Guid id);
    Task<PagedResultDto<LanguageDto>> GetListAsync(LanguageTextGetListInput input);
    Task<LanguageDto> UpdateAsync(Guid id, UpdateLanguageTextDto input);
}