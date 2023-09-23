using Microsoft.EntityFrameworkCore;
using Tiger.Module.System.Localization;
using Tiger.Module.System.Platform.Datas;
using Tiger.Module.System.Platform.Layouts;
using Tiger.Module.System.Platform.Menus;
using Tiger.Module.System.TextTemplate;
using Tiger.Module.TaskManagement;
using Tiger.Volo.Abp.Identity.Post;
using Tiger.Volo.Abp.Identity.Users;
using Tiger.Volo.Abp.Sass.Editions;
using Tiger.Volo.Abp.Sass.Tenants;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;

namespace Tiger.EntityFrameworkCore
{

    /// <summary>
    /// 自定义的DbContext
    /// </summary>
    // 配置连接字符串选择
    [ConnectionStringName("Default")]
    public class TigerDbContext : AbpDbContext<TigerDbContext>
    {

        /* This is your actual DbContext used on runtime.
         * It includes only your entities.
         * It does not include entities of the used modules, because each module has already
         * its own DbContext class. If you want to share some database tables with the used modules,
         * just create a structure like done for AppUser.
         *
         * Don't use this DbContext for database migrations since it does not contain tables of the
         * used modules (as explained above). See TigerMigrationsDbContext for migrations.
         * 
         * 模块通常使用 ConnectionStringName attribute 为 DbContext 类关联一个唯一的连接字符串名称. 示例:
         */

        public DbSet<AppUser> Users { get; set; }

        // 如何扩展abp原有的租户表 迁移里面文档了解
        public DbSet<Edition> Editions { get; set; }

        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }


        public DbSet<Post> Posts { get; set; }
        public DbSet<TextTemplate> TextTemplates { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<LanguageText> LanguageTexts { get; set; }


        public DbSet<Data> Datas { get; set; }
        public DbSet<Layout> Layouts { get; set; }
        public DbSet<Menu> Menus { get; set; }

        public DbSet<BackgroundJobInfo> BackgroundJobInfos { get; set; }
        public DbSet<BackgroundJobAction> BackgroundJobActions { get; set; }
        public DbSet<BackgroundJobLog> BackgroundJobLogs { get; set; }


        public TigerDbContext(DbContextOptions<TigerDbContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// https://docs.microsoft.com/zh-cn/ef/core/dbcontext-configuration/#other-dbcontext-configuration
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        /// <summary>
        /// 重写 OnModelCreating 方法并且做一些配置
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Always call the base method
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser

                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                /* Configure mappings for your additional properties
                 * Also see the TigerEfCoreEntityExtensionMappings class
                 */
                b.Property(x => x.Avatar).IsRequired(false).HasMaxLength(AppUserConsts.MaxAvatarLength).HasColumnName(nameof(AppUser.Avatar));
                b.Property(x => x.Introduction).IsRequired(false).HasMaxLength(AppUserConsts.MaxIntroductionLength).HasColumnName(nameof(AppUser.Introduction));
            });

            /* Configure your own tables/entities inside the ConfigureTiger method */

            builder.ConfigureTiger();
            builder.ConfigureTaskManagement();
        }

        

    }
}
