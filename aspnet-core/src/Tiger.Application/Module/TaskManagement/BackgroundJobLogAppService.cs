using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiger.Module.TaskManagement.Dtos;
using Tiger.Module.TaskManagement.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.TaskManagement;

/// <summary>
/// 作业日志
/// </summary>
[RemoteService(IsEnabled = false)]
[Authorize(TaskManagementPermissions.BackgroundJobLogs.Default)]
public class BackgroundJobLogAppService : TaskManagementApplicationService, IBackgroundJobLogAppService
{
    protected IBackgroundJobLogRepository BackgroundJobLogRepository { get; }

    public BackgroundJobLogAppService(
        IBackgroundJobLogRepository backgroundJobLogRepository)
    {
        BackgroundJobLogRepository = backgroundJobLogRepository;
    }

    /// <summary>
    /// 删除日志
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(TaskManagementPermissions.BackgroundJobLogs.Delete)]
    public virtual Task DeleteAsync(long id)
    {
        return BackgroundJobLogRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 获取作业详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async virtual Task<BackgroundJobLogDto> GetAsync(long id)
    {
        var backgroundJobLog = await BackgroundJobLogRepository.GetAsync(id);

        return ObjectMapper.Map<BackgroundJobLog, BackgroundJobLogDto>(backgroundJobLog);
    }

    /// <summary>
    /// 分页查询作业日志列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async virtual Task<PagedResultDto<BackgroundJobLogDto>> GetListAsync(BackgroundJobLogGetListInput input)
    {
        var filter = new BackgroundJobLogFilter
        {
            BeginRunTime = input.BeginRunTime,
            EndRunTime = input.EndRunTime,
            HasExceptions = input.HasExceptions,
            Filter = input.Filter,
            Group = input.Group,
            Name = input.Name,
            Type = input.Type
        };

        var totalCount = await BackgroundJobLogRepository.GetCountAsync(filter, input.JobId);
        var backgroundJobLogs = await BackgroundJobLogRepository.GetListAsync(
            filter, input.JobId, input.Sorting, input.MaxResultCount, input.SkipCount);

        return new PagedResultDto<BackgroundJobLogDto>(totalCount,
     ObjectMapper.Map<List<BackgroundJobLog>, List<BackgroundJobLogDto>>(backgroundJobLogs));
    }
}
