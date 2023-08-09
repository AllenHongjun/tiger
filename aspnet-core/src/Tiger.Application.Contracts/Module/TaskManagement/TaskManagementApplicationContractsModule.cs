using LINGYUN.Abp.Dynamic.Queryable;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Tiger.Module.TaskManagement;

[DependsOn(typeof(TaskManagementDomainSharedModule))]
[DependsOn(typeof(AbpDynamicQueryableApplicationContractsModule))]
[DependsOn(typeof(AbpDddApplicationContractsModule))]
public class TaskManagementApplicationContractsModule : AbpModule
{
}
