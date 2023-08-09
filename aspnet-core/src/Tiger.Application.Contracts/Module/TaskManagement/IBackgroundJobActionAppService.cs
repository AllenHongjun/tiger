using System;
using System.Threading.Tasks;
using Tiger.Module.TaskManagement.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.TaskManagement;

public interface IBackgroundJobActionAppService : IApplicationService
{
    Task<BackgroundJobActionDto> AddActionAsync(string jobId, BackgroundJobActionCreateDto input);

    Task<BackgroundJobActionDto> UpdateActionAsync(Guid id, BackgroundJobActionUpdateDto input);

    Task DeleteActionAsync(Guid id);

    Task<ListResultDto<BackgroundJobActionDto>> GetActionsAsync(string jobId);

    Task<ListResultDto<BackgroundJobActionDefinitionDto>> GetDefinitionsAsync(BackgroundJobActionGetDefinitionsInput input);
}
