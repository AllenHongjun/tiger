using Volo.Abp.DistributedLocking;
using Volo.Abp.Modularity;

namespace Tiger.Infrastructure.BackgroundTasks.DistributedLocking;

[DependsOn(typeof(AbpBackgroundTasksModule))]
[DependsOn(typeof(AbpDistributedLockingAbstractionsModule))]
public class AbpBackgroundTasksDistributedLockingModule : AbpModule
{

}
