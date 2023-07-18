using Tiger.Module.System.Platform.Datas;
using Tiger.Module.System.Localization;
using Tiger.Module.System.TextTemplate;
using Tiger.Volo.Abp.Identity.Post;
using Microsoft.EntityFrameworkCore;
using Tiger.Books;
using Tiger.Volo.Abp.Sass.Editions;
using Tiger.Volo.Abp.Sass;
using TigerAdmin.Books;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.TenantManagement;

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
            

            #region BookStore
            // 添加 Book 实体的映射代码 自动根据代码生成数据表 将Book实体映射到数据库表
            builder.Entity<Book>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Books",
                          TigerConsts.DbSchema);

                // /自动配置基础属性
                b.ConfigureByConvention();

                // 使用fluent API 配置其他属性
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);

                // ADD THE MAPPING FOR THE RELATION
                b.HasOne<Business.Demo.Author>()
                .WithMany()
                .HasForeignKey(x => x.AuthorId)
                .IsRequired();

                //b.HasMany(u => u.Claims).WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
                //b.HasMany(u => u.Logins).WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
                //b.HasMany(u => u.Roles).WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
                //b.HasMany(u => u.Tokens).WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
                //b.HasMany(u => u.OrganizationUnits).WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

                //b.HasIndex(u => u.NormalizedUserName);
                //b.HasIndex(u => u.NormalizedEmail);
                //b.HasIndex(u => u.UserName);
                //b.HasIndex(u => u.Email);
            });

            // 添加作者
            builder.Entity<Business.Demo.Author>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Authors",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(AuthorConsts.MaxNameLength);

                b.HasIndex(x => x.Name);


            });
            #endregion

            #region Sass
            //if (builder.IsTenantOnlyDatabase())
            //{
            //    return;
            //}
            //builder.Entity<Edition>(b =>
            //{
            //    b.ToTable(AbpSaasDbProperties.DbTablePrefix + "Editions", AbpSaasDbProperties.DbSchema);

            //    b.ConfigureByConvention();

            //    b.Property(t => t.DisplayName)
            //        .HasMaxLength(EditionConsts.MaxDisplayNameLength)
            //        .IsRequired()
            //        .HasComment("显示名称");

            //    b.HasIndex(u => u.DisplayName);

            //    //b.ApplyObjectExtensionMappings();
            //});
            //builder.Entity<Tiger.Volo.Abp.Sass.Tenants.Tenant>(b =>
            //{
            //    b.ToTable(AbpSaasDbProperties.DbTablePrefix + "Tenants", AbpSaasDbProperties.DbSchema);

            //    b.ConfigureByConvention();

            //    b.Property(t => t.Name).IsRequired().HasMaxLength(TenantConsts.MaxNameLength).HasComment("租户名称");

            //    b.HasMany(u => u.ConnectionStrings).WithOne().HasForeignKey(uc => uc.TenantId).IsRequired();

            //    b.HasIndex(u => u.Name);

            //    //b.ApplyObjectExtensionMappings();
            //});
            //builder.Entity<TenantConnectionString>(b =>
            //{
            //    b.ToTable(AbpSaasDbProperties.DbTablePrefix + "TenantConnectionStrings", AbpSaasDbProperties.DbSchema);

            //    b.ConfigureByConvention();

            //    b.HasKey(x => new { x.TenantId, x.Name });

            //    b.Property(cs => cs.Name).IsRequired().HasMaxLength(TenantConnectionStringConsts.MaxNameLength);
            //    b.Property(cs => cs.Value).IsRequired().HasMaxLength(TenantConnectionStringConsts.MaxValueLength);

            //    //b.ApplyObjectExtensionMappings();
            //});
            #endregion

            #region System
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

                    x.HasMany(p => p.Items)
                        .WithOne()
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


        }
    }
}
