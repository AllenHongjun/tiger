using Microsoft.EntityFrameworkCore;
using Tiger.Basic;
using Tiger.Books;
using Tiger.Business;
using Tiger.Business.Basic;
using Tiger.Business.Orders;
using Tiger.Orders;
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

            #region Demo模块
            //4. 添加 Book 实体的映射代码 自动根据代码生成数据表
            builder.Entity<Book>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Books",
                          TigerConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);

                // ADD THE MAPPING FOR THE RELATION
                b.HasOne<Business.Demo.Author>().WithMany().HasForeignKey(x => x.AuthorId).IsRequired();

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

            #region 基础模块
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


            // 产品分类
            builder.Entity<Category>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Categorys",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name)
                    .IsRequired();
                b.Property(x => x.Level).IsRequired().HasMaxLength(10)
                    .HasColumnName(nameof(Category.Level));
                b.Property(x => x.Keyword).HasDefaultValue(false);

                b.HasMany(t => t.Children)
                .WithOne(t => t.Parent)
                .HasForeignKey(t => t.ParentId)
                .OnDelete(DeleteBehavior.Restrict);


                

            });

            // 产品属性类型表
            builder.Entity<ProductAttributeType>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "ProductAttributeTypes",
                    TigerConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Name)
                    .IsRequired();
                b.HasIndex(x => x.Name);

            });

            // 产品属性表
            builder.Entity<ProductAttribute>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "ProductAttributes",
                    TigerConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Name)
                    .IsRequired();
                b.HasIndex(x => x.Name);

            });

            // 产品属性表
            builder.Entity<ProductAttribute>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "ProductAttributes",
                    TigerConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Name)
                    .IsRequired();
                b.HasIndex(x => x.Name);

            });

            // 产品评论表
            builder.Entity<Comment>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Comments",
                    TigerConsts.DbSchema);

                b.ConfigureByConvention();

            });

            // 产品评论回复表
            builder.Entity<CommentReply>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "CommentReplys",
                    TigerConsts.DbSchema);

                b.ConfigureByConvention();

            });

            // 产品标签关系表
            builder.Entity<ProductTagRelation>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "ProductTagRelation",
                    TigerConsts.DbSchema);

                b.ConfigureByConvention();
            }); 
            #endregion

            #region 订单模块
            //订单
            builder.Entity<Order>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Orders",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
            });

            //订单明细
            builder.Entity<OrderItem>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "OrderItems",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
            });

            //订单操作历史记录
            builder.Entity<OrderOperateHistory>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "OrderOperateHistorys",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
            });

            //订单设置
            builder.Entity<OrderSetting>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "OrderSettings",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
            }); 
            #endregion


        }
    }
}