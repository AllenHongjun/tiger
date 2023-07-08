using Tiger.Module.System.TextTemplate;
using Tiger.Volo.Abp.Identity.Post;
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
    /// ���Ǽ���EF Core����Ŀ. �������� DbContext ��ʵ�� .Domain ��Ŀ�ж���Ĳִ��ӿ�.
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

                //��Ĭ�ϲִ�ʵ���滻Ϊ�Զ���ִ�ʵ��
                options.AddRepository<Book, BookRepository>();
                
                options.AddRepository<Post, PostRepository>();
                options.AddRepository<TextTemplate, TextTemplateRepository>();
            });

            Configure<AbpDbContextOptions>(options =>
            {
                #region ����ΪӦ�ó�������� DbContextʹ��SQL Server��ΪĬ��DBMS
                /* The main point to change your DBMS.
                         * See also TigerMigrationsDbContextFactory for EF Core tooling. */
                // ��д��
                options.UseSqlServer();

                //// ʹ��Sqlserver����һ��д��
                //Configure<AbpDbContextOptions>(options =>
                //{
                //    options.Configure(opts =>
                //    {
                //        opts.UseSqlServer();
                //    });
                //});
                #endregion


                //// ���� DbContextOptions (EF Core���е�����)
                //options.Configure(opts =>
                //{
                //    opts.DbContextOptions.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                //});


                //// Ϊ������DbContext ʹ��һ����������
                //options.Configure<MyOtherDbContext>(opts =>
                //{
                //    opts.UseMySQL();
                //});
            });
        }
    }
}
