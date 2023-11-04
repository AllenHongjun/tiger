using Tiger.Module.Teachings;
using Tiger.Module.Schools;
using Tiger.Module.System.Area;
using Tiger.Module.System.Platform.Datas;
using Tiger.Module.System.Localization;
using Tiger.Module.System.TextTemplate;
using Tiger.Volo.Abp.Identity.Post;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Tiger.Module.System.Platform.Menus;
using Tiger.Module.System.Platform.Routes;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiger.Module.System.Platform.Layouts;
using Tiger.Volo.Abp.Sass.Tenants;
using Tiger.Volo.Abp.Sass;
using Tiger.Volo.Abp.Sass.Editions;

namespace Tiger.EntityFrameworkCore
{
    /// <summary>
    /// 配置数据结构表约束
    /// </summary>
    /// <remarks>
    /// EF Core 使用 fluent API 配置模型: https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0
    /// ABP框架配置实体基类的约定：https://docs.abp.io/zh-Hans/abp/latest/Entities
    /// </remarks>
    public static class TigerDbContextModelCreatingExtensions
    {
        public static void ConfigureTiger(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */
            

            #region Sass
            //if (builder.IsTenantOnlyDatabase())
            //{
            //    return;
            //}
            builder.Entity<Edition>(b =>
            {
                b.ToTable(AbpSaasDbProperties.DbTablePrefix + "Editions", AbpSaasDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(t => t.DisplayName)
                    .HasMaxLength(EditionConsts.MaxDisplayNameLength)
                    .IsRequired()
                    .HasComment("显示名称");

                b.HasIndex(u => u.DisplayName);

                //b.ApplyObjectExtensionMappings();
            });

            //重用模块的表 https://docs.abp.io/zh-Hans/abp/4.4/Entity-Framework-Core-Migrations
            // 它映射到 AbpTenents 表,与 Abp 自带的Tenant 实体共享.
            builder.Entity<Tenant>(b =>
            {
                b.ToTable(AbpSaasDbProperties.DbTablePrefix + "Tenants", AbpSaasDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(t => t.Name).IsRequired().HasMaxLength(TenantConsts.MaxNameLength).HasComment("租户名称");

                b.HasMany(u => u.ConnectionStrings).WithOne().HasForeignKey(uc => uc.TenantId).IsRequired();

                b.HasIndex(u => u.Name);

                //b.ApplyObjectExtensionMappings();
            });
            builder.Entity<TenantConnectionString>(b =>
            {
                b.ToTable(AbpSaasDbProperties.DbTablePrefix + "TenantConnectionStrings", AbpSaasDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.HasKey(x => new { x.TenantId, x.Name });

                b.Property(cs => cs.Name).IsRequired().HasMaxLength(TenantConnectionStringConsts.MaxNameLength);
                b.Property(cs => cs.Value).IsRequired().HasMaxLength(TenantConnectionStringConsts.MaxValueLength);
                
                //b.ApplyObjectExtensionMappings();
            });
            #endregion

            #region System



            builder.Entity<Layout>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Layouts", TigerConsts.DbSchema);

                b.Property(p => p.Framework)
                    .HasMaxLength(LayoutConsts.MaxFrameworkLength)
                    .HasColumnName(nameof(Layout.Framework))
                    .IsRequired();

                b.ConfigureRoute();
            });

            #region Menu
            builder.Entity<Menu>(b =>
                {
                    b.ToTable(TigerConsts.DbTablePrefix + "Menus", TigerConsts.DbSchema);

                    b.ConfigureRoute();

                    b.Property(p => p.Framework)
                        .HasMaxLength(LayoutConsts.MaxFrameworkLength)
                        .HasColumnName(nameof(Menu.Framework))
                        .IsRequired()
                        .HasComment("框架");
                    b.Property(p => p.Component)
                        .HasMaxLength(MenuConsts.MaxComponentLength)
                        .HasColumnName(nameof(Menu.Component))
                        .IsRequired()
                        .HasComment("组件");
                    b.Property(p => p.Code)
                        .HasMaxLength(MenuConsts.MaxCodeLength)
                        .HasColumnName(nameof(Menu.Code))
                        .IsRequired()
                        .HasComment("菜单编码");
                });

            builder.Entity<RoleMenu>(x =>
            {
                x.ToTable(TigerConsts.DbTablePrefix  +  "RoleMenus", TigerConsts.DbSchema);

                x.Property(p => p.RoleName)
                    .IsRequired()
                    .HasMaxLength(RoleRouteConsts.MaxRoleNameLength)
                    .HasColumnName(nameof(RoleMenu.RoleName));

                x.ConfigureByConvention();

                x.HasIndex(i => new { i.RoleName, i.MenuId });
            });

            builder.Entity<UserMenu>(x =>
            {
                x.ToTable(TigerConsts.DbTablePrefix  + "UserMenus", TigerConsts.DbSchema);

                x.ConfigureByConvention();

                x.HasIndex(i => new { i.UserId, i.MenuId });
            }); 
            #endregion

            builder.Entity<Post>(b =>
                {
                    b.ToTable(TigerConsts.DbTablePrefix + "Posts", TigerConsts.DbSchema);
                    b.ConfigureByConvention();


                    /* Configure more properties here */
                });


            builder.Entity<TextTemplate>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "TextTemplates", TigerConsts.DbSchema);
                b.Property(x => x.Name).IsRequired().HasMaxLength(256).HasComment("名称");
                b.Property(x => x.DisplayName).HasMaxLength(256).HasComment("显示名称");
                b.Property(x => x.Content).HasMaxLength(1024 * 4).HasComment("模板内容");
                b.Property(x => x.Culture).HasMaxLength(256).HasComment("文化名称");
                b.ConfigureByConvention();
            });


            #region Localization Language
            builder.Entity<Language>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Languages", TigerConsts.DbSchema);
                b.Property(e => e.CultureName).IsRequired().HasMaxLength(128).HasComment("语言名称");
                b.Property(e => e.UiCultureName).IsRequired().HasMaxLength(128).HasComment("Ui语言名称");
                b.Property(e => e.DisplayName).IsRequired().HasMaxLength(128).HasComment("显示名称");
                b.Property(e => e.FlagIcon).HasMaxLength(128).HasComment("图标");
                b.Property<bool>(x => x.IsEnabled).IsRequired();
                b.HasIndex(e => e.CultureName).IsUnique();
                b.ConfigureByConvention();


                /* Configure more properties here */
            });


            builder.Entity<Resource>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Resources", TigerConsts.DbSchema);
                b.ConfigureByConvention();


                /* Configure more properties here */
            });


            builder.Entity<LanguageText>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "LanguageTexts", TigerConsts.DbSchema);
                b.Property(e => e.ResourceName).IsRequired().HasMaxLength(128).HasComment("资源名称");
                b.Property(e => e.CultrueName).IsRequired().HasMaxLength(128).HasComment("语言名称");
                b.Property(e => e.Key).IsRequired().HasMaxLength(256).HasComment("键名称");
                b.Property(e => e.Value).IsRequired().HasMaxLength(256).HasComment("值");
                b.HasIndex(x => new { x.TenantId, x.ResourceName, x.CultrueName });
                b.ConfigureByConvention();


                /* Configure more properties here */
            });
            #endregion
            #endregion

            #region Data
            builder.Entity<Data>(x =>
                {
                    x.ToTable(TigerConsts.DbTablePrefix + "Datas", TigerConsts.DbSchema);

                    x.Property(p => p.Code)
                        .HasMaxLength(DataConsts.MaxCodeLength)
                        .HasColumnName(nameof(Data.Code))
                        .IsRequired()
                        .HasComment("编码");
                    x.Property(p => p.Name)
                        .HasMaxLength(DataConsts.MaxNameLength)
                        .HasColumnName(nameof(Data.Name))
                        .IsRequired()
                        .HasComment("数据字典名称");
                    x.Property(p => p.DisplayName)
                       .HasMaxLength(DataConsts.MaxDisplayNameLength)
                       .HasColumnName(nameof(Data.DisplayName))
                       .IsRequired()
                       .HasComment("数据字典显示名称");
                    x.Property(p => p.Description)
                        .HasMaxLength(DataConsts.MaxDescriptionLength)
                        .HasColumnName(nameof(Data.Description))
                        .HasComment("数据字典描述");

                    x.ConfigureByConvention();

                    //EFcore 一对多关系配置 https://learn.microsoft.com/zh-cn/ef/core/modeling/relationships/one-to-many
                    x.HasMany(p => p.Items)
                        .WithOne(p => p.Data)
                        .HasForeignKey(fk => fk.DataId)
                        .IsRequired();

                    x.HasIndex(i => new { i.Name });
                });

            builder.Entity<DataItem>(x =>
            {
                x.ToTable(TigerConsts.DbTablePrefix + "DataItems", TigerConsts.DbSchema);

                x.Property(p => p.DefaultValue)
                    .HasMaxLength(DataItemConsts.MaxValueLength)
                    .HasColumnName(nameof(DataItem.DefaultValue))
                    .HasComment("默认值");
                x.Property(p => p.Name)
                    .HasMaxLength(DataItemConsts.MaxNameLength)
                    .HasColumnName(nameof(DataItem.Name))
                    .IsRequired()
                    .HasComment("字典数据名称");
                x.Property(p => p.DisplayName)
                   .HasMaxLength(DataItemConsts.MaxDisplayNameLength)
                   .HasColumnName(nameof(DataItem.DisplayName))
                   .IsRequired()
                   .HasComment("显示名称");
                x.Property(p => p.Description)
                    .HasMaxLength(DataItemConsts.MaxDescriptionLength)
                    .HasColumnName(nameof(DataItem.Description))
                    .HasComment("描述");

                x.Property(p => p.AllowBeNull).HasDefaultValue(true);

                x.ConfigureByConvention();

                x.HasIndex(i => new { i.Name });
            });
            #endregion




            builder.Entity<Region>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Regions", TigerConsts.DbSchema);
                b.ConfigureByConvention(); 
            });


            builder.Entity<School>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Schools", TigerConsts.DbSchema);
                b.Property(p => p.Name)
                    .HasMaxLength(SchoolConsts.MaxNameLength)
                    .IsRequired()
                    .HasColumnName(nameof(School.Name))
                    .HasComment("名称");
                b.Property(p => p.ShortName)
                    .HasMaxLength(SchoolConsts.MaxShortNameLength)
                    .HasColumnName(nameof(School.ShortName))
                    .HasComment("简称");
                b.Property(p => p.Sort)
                    .HasComment("顺序");
                b.Property(p => p.IsEnable)
                    .HasComment("是否启用");
                b.Property(p => p.Number).HasMaxLength(SchoolConsts.MaxShortNameLength)
                    .HasComment("编号");
                b.Property(p => p.ImpowerDate)
                    .HasComment("授权截止时间");
                b.Property(p => p.MaxPerson)
                    .HasComment("最大人数");
                b.Property(p => p.IsAudit)
                    .HasComment("是否需要审核帖子");
                b.Property(p => p.VipLevel)
                    .HasComment("Vip等级：1.免费客户 2.付费客户");

                b.HasMany(p => p.ClassInfos)
                        .WithOne(p => p.School)
                        .HasForeignKey(fk => fk.SchoolId)
                        .IsRequired();

                b.ConfigureByConvention(); 
            });


            builder.Entity<ClassInfo>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Classes", TigerConsts.DbSchema);
                b.Property(p => p.Name)
                    .HasMaxLength(ClassConsts.MaxNameLength)
                    .IsRequired()
                    .HasComment("名称");
                b.Property(p => p.SchoolId)
                    .HasComment("学校Id");
                b.Property(p => p.Sorting)
                    .HasComment("顺序");
                b.Property(p => p.Sorting)
                    .HasComment("是否启用");

                b.ConfigureByConvention(); 

            });


            builder.Entity<Course>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Courses", TigerConsts.DbSchema);
                b.Property(p => p.Name)
                   .HasMaxLength(CourseConsts.MaxNameLength)
                   .IsRequired()
                   .HasComment("名称");
                b.Property(p => p.Description)
                   .HasMaxLength(CourseConsts.MaxDescriptionLength)
                   .IsRequired()
                   .HasComment("描述");
                b.Property(p => p.Cover)
                    .HasMaxLength(CourseConsts.MaxCoverLenght)
                    .HasComment("封面图片");
                b.ConfigureByConvention();

                b.HasMany(p => p.TestPaper)
                .WithOne(p => p.Course)
                .HasForeignKey(p => p.CourseId)
                .IsRequired(false);

                /* Configure more properties here */
            });
        }



        public static EntityTypeBuilder<TRoute> ConfigureRoute<TRoute>(
            this EntityTypeBuilder<TRoute> builder)
            where TRoute : Route
        {
            builder
                .Property(p => p.DisplayName)
                .HasMaxLength(RouteConsts.MaxDisplayNameLength)
                .HasColumnName(nameof(Route.DisplayName))
                .IsRequired();
            builder
                .Property(p => p.Name)
                .HasMaxLength(RouteConsts.MaxNameLength)
                .HasColumnName(nameof(Route.Name))
                .IsRequired();
            builder
                .Property(p => p.Path)
                .HasMaxLength(RouteConsts.MaxPathLength)
                .HasColumnName(nameof(Route.Path));
            builder
                .Property(p => p.Redirect)
                .HasMaxLength(RouteConsts.MaxRedirectLength)
                .HasColumnName(nameof(Route.Redirect));

            builder
                .Property(p => p.Status)
                .HasColumnName(nameof(Route.Status))
                .IsRequired();

            builder.ConfigureByConvention();

            return builder;
        }

        public static OwnedNavigationBuilder<TEntity, TRoute> ConfigureRoute<TEntity, TRoute>(
            [NotNull] this OwnedNavigationBuilder<TEntity, TRoute> builder,
            [CanBeNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            where TEntity : class
            where TRoute : Route
        {
            builder.ToTable(tablePrefix + "Routes", schema);

            builder
                .Property(p => p.DisplayName)
                .HasMaxLength(RouteConsts.MaxDisplayNameLength)
                .HasColumnName(nameof(Route.DisplayName))
                .IsRequired();
            builder
                .Property(p => p.Name)
                .HasMaxLength(RouteConsts.MaxNameLength)
                .HasColumnName(nameof(Route.Name))
                .IsRequired();
            builder
                .Property(p => p.Path)
                .HasMaxLength(RouteConsts.MaxPathLength)
                .HasColumnName(nameof(Route.Path));
            builder
                .Property(p => p.Redirect)
                .HasMaxLength(RouteConsts.MaxRedirectLength)
                .HasColumnName(nameof(Route.Redirect));

            return builder;
        }
    }
}
