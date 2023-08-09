using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Module.TaskManagement.Dtos;
using Tiger.Module.TaskManagement.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.TaskManagement;

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

    [HttpPost]
    [Route("{jobId}")]
    public Task<BackgroundJobActionDto> AddActionAsync(string jobId, BackgroundJobActionCreateDto input)
    {
        return Service.AddActionAsync(jobId, input);
    }

    [HttpDelete]
    [Route("{id}")]
    public Task DeleteActionAsync(Guid id)
    {
        return Service.DeleteActionAsync(id);
    }

    [HttpGet]
    [Route("{jobId}")]
    public Task<ListResultDto<BackgroundJobActionDto>> GetActionsAsync(string jobId)
    {
        return Service.GetActionsAsync(jobId);
    }

    [HttpGet]
    [Route("definitions")]
    public Task<ListResultDto<BackgroundJobActionDefinitionDto>> GetDefinitionsAsync(BackgroundJobActionGetDefinitionsInput input)
    {
        return Service.GetDefinitionsAsync(input);
    }

    [HttpPut]
    [Route("{id}")]
    public Task<BackgroundJobActionDto> UpdateActionAsync(Guid id, BackgroundJobActionUpdateDto input)
    {
        return Service.UpdateActionAsync(id, input);
    }
}
