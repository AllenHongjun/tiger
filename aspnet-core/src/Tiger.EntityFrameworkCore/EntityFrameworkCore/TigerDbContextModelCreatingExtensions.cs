using Microsoft.EntityFrameworkCore;
using Tiger.Books;
using TigerAdmin.Books;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

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

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(TigerConsts.DbTablePrefix + "YourEntities", TigerConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});

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

            
            
        }
    }
}
