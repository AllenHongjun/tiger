using Tiger.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Tiger.DbMigrator
{
    /// <summary>
    /// 这是一个控制台应用程序,它简化了在开发和生产环境执行数据库迁移的操作.当你使用它时;
    /// 这个项目有自己的 appsettings.json 文件. 所以如果要更改数据库连接字符串,请记得也要更改此文件.
    ///     必要时创建数据库(没有数据库时).
    ///     应用未迁移的数据库迁移.
    ///     初始化种子数据(当你需要时).
    /// </summary>
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
