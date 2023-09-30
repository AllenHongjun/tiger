using System.Collections.Generic;
using System.Threading.Tasks;
using Tiger.Module.TaskManagement.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.TaskManagement;

public interface IBackgroundJobInfoAppService :
    ICrudAppService<
        BackgroundJobInfoDto,
        string,
        BackgroundJobInfoGetListInput,
        BackgroundJobInfoCreateDto,
        BackgroundJobInfoUpdateDto>,
    IApplicationService
{
    /// <summary>
    /// 获取所有作业分组
    /// </summary>
    /// <returns></returns>
    List<string> GetAllGroups();

    Task TriggerAsync(string id);

    Task PauseAsync(string id);

    Task ResumeAsync(string id);

    Task StopAsync(string id);

    Task StartAsync(string id);

    Task BulkDeleteAsync(BackgroundJobInfoBatchInput input);

    Task BulkStopAsync(BackgroundJobInfoBatchInput input);

    Task BulkStartAsync(BackgroundJobInfoBatchInput input);

    Task BulkTriggerAsync(BackgroundJobInfoBatchInput input);

    Task BulkResumeAsync(BackgroundJobInfoBatchInput input);

    Task BulkPauseAsync(BackgroundJobInfoBatchInput input);

    Task<ListResultDto<BackgroundJobDefinitionDto>> GetDefinitionsAsync();
}
