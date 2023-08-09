using Tiger.Module.TaskManagement.Localization;
using Volo.Abp.Application.Services;

namespace Tiger.Module.TaskManagement;

public abstract class TaskManagementApplicationService : ApplicationService
{
    protected TaskManagementApplicationService()
    {
        LocalizationResource = typeof(TaskManagementResource);
        ObjectMapperContext = typeof(TaskManagementApplicationModule);
    }
}
