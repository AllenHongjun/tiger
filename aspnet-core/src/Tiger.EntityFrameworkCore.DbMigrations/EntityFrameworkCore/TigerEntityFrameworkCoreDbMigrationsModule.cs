using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Tiger.EntityFrameworkCore
{
    /// <summary>
    /// 该项目包含解决方案的EF Core数据库迁移. 它有独立的 DbContext 来专门管理迁移.
    /// EF迁移数据库的时候使用 https://docs.abp.io/zh-Hans/abp/latest/Entity-Framework-Core-Migrations
    /// </summary>
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
