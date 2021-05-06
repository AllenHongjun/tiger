using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace TigerAdmin.EntityFrameworkCore
{
    [DependsOn(
        typeof(TigerAdminEntityFrameworkCoreModule)
        )]
    public class TigerAdminEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<TigerAdminMigrationsDbContext>();
        }
    }
}
