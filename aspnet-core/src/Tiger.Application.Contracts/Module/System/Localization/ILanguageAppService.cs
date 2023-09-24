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
    Task<LanguageDto> CreateAsync(CreateLanguageDto input);
    Task DeleteAsync(Guid id);
    Task<PagedResultDto<LanguageDto>> GetListAsync(LanguageGetListInput input);
    Task<LanguageDto> UpdateAsync(Guid id, UpdateLanguageDto input);

}