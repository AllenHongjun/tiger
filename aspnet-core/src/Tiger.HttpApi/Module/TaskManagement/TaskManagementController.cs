using Tiger.Module.TaskManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;

namespace Tiger.Module.TaskManagement;

public abstract class TaskManagementController : AbpController
{
    protected TaskManagementController()
    {
        LocalizationResource = typeof(TaskManagementResource);
    }
}
