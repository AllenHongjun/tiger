using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Tiger.Module.TaskManagement;

//[DependsOn(typeof(TaskManagementApplicationContractsModule))]
//[DependsOn(typeof(AbpDynamicQueryableApplicationModule))]
//[DependsOn(typeof(TaskManagementDomainModule))]
[DependsOn(typeof(AbpDddApplicationModule))]
public class TaskManagementApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<TaskManagementApplicationModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<TaskManagementApplicationMapperProfile>(validate: true);
        });
    }
}
