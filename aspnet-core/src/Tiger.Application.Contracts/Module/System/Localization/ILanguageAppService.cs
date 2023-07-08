using System;
using System.Threading.Tasks;
using Tiger.Module.System.Localization.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.System.Localization;

/// <summary>
/// ���Թ������ӿ�
/// </summary>
public interface ILanguageAppService : IApplicationService
{
    Task<LanguageTextDto> CreateAsync(LanguageTextDto input);
    Task DeleteAsync(Guid id);
    Task<PagedResultDto<LanguageTextDto>> GetListAsync(LanguageGetListInput input);
    Task<LanguageTextDto> UpdateAsync(Guid id, UpdateLanguageDto input);

}