using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tiger.Books;
using Tiger.Demo;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Tiger.EntityFrameworkCore
{
    /// <summary>
    /// 这是集成EF Core的项目. 它定义了 DbContext 并实现 .Domain 项目中定义的仓储接口.
    /// </summary>
    [DependsOn(
        typeof(TigerDomainModule),
        typeof(AbpIdentityEntityFrameworkCoreModule),
        typeof(AbpIdentityServerEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpTenantManagementEntityFrameworkCoreModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule)
        )]
    public class TigerEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            TigerEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<TigerDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);

                //将默认仓储实现替换为自定义仓储实现
                options.AddRepository<Book, BookRepository>();
                
            });

            Configure<AbpDbContextOptions>(options =>
            {
                #region 配置为应用程序的所有 DbContext使用SQL Server作为默认DBMS
                /* The main point to change your DBMS.
                         * See also TigerMigrationsDbContextFactory for EF Core tooling. */
                // 简化写法
                options.UseSqlServer();

                //// 使用Sqlserver另外一种写法
                //Configure<AbpDbContextOptions>(options =>
                //{
                //    options.Configure(opts =>
                //    {
                //        opts.UseSqlServer();
                //    });
                //});
                #endregion


                //// 设置 DbContextOptions (EF Core自有的配置)
                //options.Configure(opts =>
                //{
                //    opts.DbContextOptions.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                //});


                //// 为其他的DbContext 使用一个特殊配置
                //options.Configure<MyOtherDbContext>(opts =>
                //{
                //    opts.UseMySQL();
                //});
            });
        }
    }
}
