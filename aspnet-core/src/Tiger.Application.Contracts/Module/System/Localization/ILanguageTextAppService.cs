using System;
using System.Threading.Tasks;
using Tiger.Module.System.Localization.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.System.Localization;

public interface ILanguageTextAppService :IApplicationService
{
    Task<LanguageTextDto> CreateAsync(CreateLanguageTextDto input);
    Task DeleteAsync(Guid id);
    Task<PagedResultDto<LanguageTextDto>> GetListAsync(LanguageTextGetListInput input);
    Task<LanguageTextDto> UpdateAsync(Guid id, UpdateLanguageTextDto input);
}