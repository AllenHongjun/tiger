using System;
using System.Threading.Tasks;
using Tiger.Module.System.TextTemplate.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.System.TextTemplate;

public interface ITextTemplateAppService :
    IApplicationService
{

    Task<TextTemplateDefinitionDto> GetAsync(string name);

    Task<TextTemplateContentDto> GetContentAsync(TextTemplateContentGetInput input);

    Task RestoreToDefaultAsync(TextTemplateRestoreInput restoreInput);

    Task<TextTemplateDefinitionDto> UpdateAsync(TextTemplateUpdateInput input);

    Task<PagedResultDto<TextTemplateDefinitionDto>> GetListAsync(TextTemplateGetListInput input);
}