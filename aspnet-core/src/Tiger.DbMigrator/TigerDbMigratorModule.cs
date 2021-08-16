using Tiger.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Tiger.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(TigerEntityFrameworkCoreDbMigrationsModule),
        typeof(TigerApplicationContractsModule)
        )]
    public class TigerDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
