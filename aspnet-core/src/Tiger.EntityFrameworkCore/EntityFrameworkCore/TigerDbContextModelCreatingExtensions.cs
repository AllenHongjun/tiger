using Microsoft.EntityFrameworkCore;
using Tiger.Books;
using Tiger.Business.Demo;
using TigerAdmin.Books;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Tiger.EntityFrameworkCore
{   
    /// <summary>
    /// 配置数据结构表约束
    /// </summary>
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

            //4. 添加 Book 实体的映射代码 自动根据代码生成数据表
            builder.Entity<Book>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Books",
                          TigerConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);

                // ADD THE MAPPING FOR THE RELATION
                b.HasOne<Author>().WithMany().HasForeignKey(x => x.AuthorId).IsRequired();
            });

            // 添加作者
            builder.Entity<Author>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Authors",
                    TigerConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(AuthorConsts.MaxNameLength);

                b.HasIndex(x => x.Name);

                
            });

            // 产品表
            builder.Entity<Product>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Products",
                    TigerConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Name)
                    .IsRequired();
                    //.HasMaxLength(AuthorConsts.MaxNameLength);

                b.HasIndex(x => x.Name);


            });
        }
    }
}