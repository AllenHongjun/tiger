using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Tiger.EntityFrameworkCore
{
    [DependsOn(
        typeof(TigerEntityFrameworkCoreModule)
        )]
    public class TigerEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<TigerMigrationsDbContext>();
        }
    }
}
