using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tiger.Module.TaskManagement.Dtos;
using Tiger.Module.TaskManagement.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.TaskManagement;

/// <summary>
/// 作业日志
/// </summary>
[RemoteService(Name = TaskManagementRemoteServiceConsts.RemoteServiceName)]
[Area(TaskManagementRemoteServiceConsts.ModuleName)]
[Authorize(TaskManagementPermissions.BackgroundJobLogs.Default)]
[Route($"api/{TaskManagementRemoteServiceConsts.ModuleName}/background-jobs/logs")]
public class BackgroundJobLogController : TaskManagementController, IBackgroundJobLogAppService
{
    protected IBackgroundJobLogAppService BackgroundJobLogAppService { get; }

    public BackgroundJobLogController(
        IBackgroundJobLogAppService backgroundJobLogAppService)
    {
        BackgroundJobLogAppService = backgroundJobLogAppService;
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    [Authorize(TaskManagementPermissions.BackgroundJobLogs.Delete)]
    public Task DeleteAsync(long id)
    {
        return BackgroundJobLogAppService.DeleteAsync(id);
    }

    /// <summary>
    /// 获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    public Task<BackgroundJobLogDto> GetAsync(long id)
    {
        return BackgroundJobLogAppService.GetAsync(id);
    }

    /// <summary>
    /// 分页列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    public Task<PagedResultDto<BackgroundJobLogDto>> GetListAsync(BackgroundJobLogGetListInput input)
    {
        return BackgroundJobLogAppService.GetListAsync(input);
    }
}
