using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tiger.Module.TaskManagement.Dtos;
using Tiger.Module.TaskManagement.Localization;
using Tiger.Module.TaskManagement.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Module.TaskManagement;

/// <summary>
/// 作业管理
/// </summary>
[RemoteService(Name = TaskManagementRemoteServiceConsts.RemoteServiceName)]
[Area(TaskManagementRemoteServiceConsts.ModuleName)]
[Route($"api/{TaskManagementRemoteServiceConsts.ModuleName}/background-jobs")]
public class BackgroundJobInfoController : AbpController, IBackgroundJobInfoAppService
{
    protected IBackgroundJobInfoAppService BackgroundJobInfoAppService { get; }

    public BackgroundJobInfoController(
        IBackgroundJobInfoAppService backgroundJobInfoAppService)
    {
        BackgroundJobInfoAppService = backgroundJobInfoAppService;

        LocalizationResource = typeof(TaskManagementResource);
    }

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(TaskManagementPermissions.BackgroundJobs.Create)]
    public virtual Task<BackgroundJobInfoDto> CreateAsync(BackgroundJobInfoCreateDto input)
    {
        return BackgroundJobInfoAppService.CreateAsync(input);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    [Authorize(TaskManagementPermissions.BackgroundJobs.Delete)]
    public virtual Task DeleteAsync(string id)
    {
        return BackgroundJobInfoAppService.DeleteAsync(id);
    }

    /// <summary>
    /// 获取详细
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    public virtual Task<BackgroundJobInfoDto> GetAsync(string id)
    {
        return BackgroundJobInfoAppService.GetAsync(id);
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    public virtual Task<PagedResultDto<BackgroundJobInfoDto>> GetListAsync(BackgroundJobInfoGetListInput input)
    {
        return BackgroundJobInfoAppService.GetListAsync(input);
    }

    /// <summary>
    /// 暂停
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}/pause")]
    [Authorize(TaskManagementPermissions.BackgroundJobs.Pause)]
    public virtual Task PauseAsync(string id)
    {
        return BackgroundJobInfoAppService.PauseAsync(id);
    }

    /// <summary>
    /// 重启
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}/resume")]
    [Authorize(TaskManagementPermissions.BackgroundJobs.Resume)]
    public virtual Task ResumeAsync(string id)
    {
        return BackgroundJobInfoAppService.ResumeAsync(id);
    }

    /// <summary>
    /// 触发
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}/trigger")]
    [Authorize(TaskManagementPermissions.BackgroundJobs.Trigger)]
    public virtual Task TriggerAsync(string id)
    {
        return BackgroundJobInfoAppService.TriggerAsync(id);
    }

    /// <summary>
    /// 停止
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}/stop")]
    [Authorize(TaskManagementPermissions.BackgroundJobs.Stop)]
    public virtual Task StopAsync(string id)
    {
        return BackgroundJobInfoAppService.StopAsync(id);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    [Authorize(TaskManagementPermissions.BackgroundJobs.Update)]
    public virtual Task<BackgroundJobInfoDto> UpdateAsync(string id, BackgroundJobInfoUpdateDto input)
    {
        return BackgroundJobInfoAppService.UpdateAsync(id, input);
    }

    /// <summary>
    /// 批量启动
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}/start")]
    [Authorize(TaskManagementPermissions.BackgroundJobs.Start)]
    public virtual Task StartAsync(string id)
    {
        return BackgroundJobInfoAppService.StartAsync(id);
    }

    /// <summary>
    /// 批量停止
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("bulk-stop")]
    [Authorize(TaskManagementPermissions.BackgroundJobs.Stop)]
    public virtual Task BulkStopAsync(BackgroundJobInfoBatchInput input)
    {
        return BackgroundJobInfoAppService.BulkStopAsync(input);
    }

    /// <summary>
    /// 批量启动
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("bulk-start")]
    [Authorize(TaskManagementPermissions.BackgroundJobs.Start)]
    public virtual Task BulkStartAsync(BackgroundJobInfoBatchInput input)
    {
        return BackgroundJobInfoAppService.BulkStartAsync(input);
    }

    /// <summary>
    /// 批量触发
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("bulk-trigger")]
    [Authorize(TaskManagementPermissions.BackgroundJobs.Trigger)]
    public virtual Task BulkTriggerAsync(BackgroundJobInfoBatchInput input)
    {
        return BackgroundJobInfoAppService.BulkTriggerAsync(input);
    }

    /// <summary>
    /// 批量重启
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("bulk-resume")]
    [Authorize(TaskManagementPermissions.BackgroundJobs.Resume)]
    public virtual Task BulkResumeAsync(BackgroundJobInfoBatchInput input)
    {
        return BackgroundJobInfoAppService.BulkResumeAsync(input);
    }

    /// <summary>
    /// 批量暂停
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("bulk-pause")]
    [Authorize(TaskManagementPermissions.BackgroundJobs.Pause)]
    public virtual Task BulkPauseAsync(BackgroundJobInfoBatchInput input)
    {
        return BackgroundJobInfoAppService.BulkPauseAsync(input);
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("bulk-delete")]
    public virtual Task BulkDeleteAsync(BackgroundJobInfoBatchInput input)
    {
        return BackgroundJobInfoAppService.BulkDeleteAsync(input);
    }

    /// <summary>
    /// 获取作业定义
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("definitions")]
    public virtual Task<ListResultDto<BackgroundJobDefinitionDto>> GetDefinitionsAsync()
    {
        return BackgroundJobInfoAppService.GetDefinitionsAsync();
    }
}
