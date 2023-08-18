using Microsoft.EntityFrameworkCore;
using Tiger.Module.TaskManagement;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Tiger.EntityFrameworkCore
{
    /* This DbContext is only used for database migrations.
     * It is not used on runtime. See TigerDbContext for the runtime DbContext.
     * It is a unified model that includes configuration for
     * all used modules and your application.
     */
    public class TigerMigrationsDbContext : AbpDbContext<TigerMigrationsDbContext>
    {
        public TigerMigrationsDbContext(DbContextOptions<TigerMigrationsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentity();
            builder.ConfigureIdentityServer();
            builder.ConfigureFeatureManagement();

            // 重写abp tenantManageManage模块的表。添加自定义的字段
            //builder.ConfigureTenantManagement();

            /* Configure your own tables/entities inside the ConfigureTiger method */

            builder.ConfigureTiger();
            builder.ConfigureTaskManagement();
        }
    }
}