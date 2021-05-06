using TigerAdmin.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace TigerAdmin.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(TigerAdminEntityFrameworkCoreDbMigrationsModule),
        typeof(TigerAdminApplicationContractsModule)
        )]
    public class TigerAdminDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
