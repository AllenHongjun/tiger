using Volo.Abp.EventBus;
using Volo.Abp.Modularity;

namespace Tiger.Infrastructure.BackgroundTasks.EventBus;

[DependsOn(typeof(AbpEventBusModule))]
[DependsOn(typeof(AbpBackgroundTasksModule))]
public class AbpBackgroundTasksEventBusModule : AbpModule
{
}
