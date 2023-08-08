using Volo.Abp.Collections;

namespace Tiger.Infrastructure.BackgroundTasks.Activities;

public class AbpBackgroundTasksActivitiesOptions
{
    public ITypeList<IJobActionDefinitionProvider> DefinitionProviders { get; }

    public AbpBackgroundTasksActivitiesOptions()
    {
        DefinitionProviders = new TypeList<IJobActionDefinitionProvider>();
    }
}
