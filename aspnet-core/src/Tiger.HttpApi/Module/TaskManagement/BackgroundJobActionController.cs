using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Module.TaskManagement.Dtos;
using Tiger.Module.TaskManagement.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.TaskManagement;

/// <summary>
/// 作业行为
/// </summary>
[RemoteService(Name = TaskManagementRemoteServiceConsts.RemoteServiceName)]
[Area(TaskManagementRemoteServiceConsts.ModuleName)]
[Authorize(TaskManagementPermissions.BackgroundJobs.ManageActions)]
[Route($"api/{TaskManagementRemoteServiceConsts.ModuleName}/background-jobs/actions")]
public class BackgroundJobActionController : TaskManagementController, IBackgroundJobActionAppService
{
    protected IBackgroundJobActionAppService Service { get; }

    public BackgroundJobActionController(
        IBackgroundJobActionAppService service)
    {
        Service = service;
    }

    /// <summary>
    /// 添加作业行为
    /// </summary>
    /// <param name="jobId"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("{jobId}")]
    public Task<BackgroundJobActionDto> AddActionAsync(string jobId, BackgroundJobActionCreateDto input)
    {
        return Service.AddActionAsync(jobId, input);
    }

    /// <summary>
    /// 删除作业行为
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public Task DeleteActionAsync(Guid id)
    {
        return Service.DeleteActionAsync(id);
    }

    /// <summary>
    /// 获取作业行为详情
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{jobId}")]
    public Task<ListResultDto<BackgroundJobActionDto>> GetActionsAsync(string jobId)
    {
        return Service.GetActionsAsync(jobId);
    }

    /// <summary>
    /// 获取作业行为定义
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("definitions")]
    public Task<ListResultDto<BackgroundJobActionDefinitionDto>> GetDefinitionsAsync(BackgroundJobActionGetDefinitionsInput input)
    {
        return Service.GetDefinitionsAsync(input);
    }

    /// <summary>
    /// 更新作业行为
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    public Task<BackgroundJobActionDto> UpdateActionAsync(Guid id, BackgroundJobActionUpdateDto input)
    {
        return Service.UpdateActionAsync(id, input);
    }
}
