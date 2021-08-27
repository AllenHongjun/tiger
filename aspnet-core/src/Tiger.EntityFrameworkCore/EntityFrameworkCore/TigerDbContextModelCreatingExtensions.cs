using Microsoft.EntityFrameworkCore;
using Tiger.Basic;
using Tiger.Books;
using Tiger.Business;
using Tiger.Business.Basic;
using Tiger.Business.Orders;
using Tiger.Business.Members;
using Tiger.Marketing;
using Tiger.Orders;
using TigerAdmin.Books;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Tiger.Stock;
using Tiger.Business.Stocks;

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

                b.HasMany(t => t.CartItems)
                .WithOne(t => t.Product)
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

                // 配置外键 禁用级联删除
                b.HasMany(t => t.Skus)
                .WithOne(t => t.Product)
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

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

                //b.HasMany(x => x.Children)
                //.WithOne(x => x.Parent)
                //.OnDelete(DeleteBehavior.Cascade);



            });

            // Sku表
            builder.Entity<Sku>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Skus",
                    TigerConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Price)
                    .IsRequired();
                //.HasMaxLength(AuthorConsts.MaxNameLength);

                b.HasOne(x => x.Product)
                .WithMany(x => x.Skus)
                .OnDelete(DeleteBehavior.Restrict);

                b.HasIndex(x => x.SkuCode);

            });

            // 店铺表
            builder.Entity<Store>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Stores",
                    TigerConsts.DbSchema);

                b.ConfigureByConvention();
            });

            // 供应商
            builder.Entity<Supply>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Supplies",
                    TigerConsts.DbSchema);

                b.ConfigureByConvention();
            });

            // 仓库
            builder.Entity<Warehouse>(b => {
                b.ToTable(TigerConsts.DbTableStockPrefix + "Warehouses");
                b.ConfigureByConvention();
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

            // 产品属性值表
            builder.Entity<ProductAttributeValue>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "ProductAttributeValues",
                    TigerConsts.DbSchema);

                b.ConfigureByConvention();

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

            // 购物车
            builder.Entity<CartItem>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "CartItems",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasOne(t => t.Product)
                .WithMany(t => t.CartItems)
                .HasForeignKey(t => t.ProductId)
                // 使用Fluent API禁用级联删除
                .OnDelete(DeleteBehavior.Restrict);
            });

            //订单
            builder.Entity<Order>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Orders",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();

                //b.HasMany(t => t.OrderReturnDetails)
                //.WithOne(t => t.Order)
                //.HasForeignKey(t => t.OrderId)
                //.OnDelete(DeleteBehavior.Restrict);
            });

            //订单明细
            builder.Entity<OrderItem>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "OrderItems",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
            });

            //订单退款明细
            //builder.Entity<OrderReturnDetail>(b =>
            //{
            //    b.ToTable(TigerConsts.DbTablePrefix + "OrderReturnDetails",
            //        TigerConsts.DbSchema);
            //    b.ConfigureByConvention();

            //    b.HasOne(t => t.Order)
            //    .WithMany(t => t.OrderReturnDetails)
            //    .HasForeignKey(t => t.OrderId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //});

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

            #region 仓储模块
            builder.Entity<Inventory>(b =>
            {
                b.ToTable(TigerConsts.DbTableStockPrefix + "Inventory");

                b.ConfigureByConvention();
            });
            builder.Entity<InventoryHistory>(b =>
            {

                b.ToTable(TigerConsts.DbTableStockPrefix + "InventoryHistory");

                b.ConfigureByConvention();
            });

            builder.Entity<BomDetail>(b => {
                b.ToTable(TigerConsts.DbTableStockPrefix + "BomDetail");
                b.ConfigureByConvention();
            });
            builder.Entity<BomHeader>(b => {
                b.ToTable(TigerConsts.DbTableStockPrefix + "BomHeader");
                b.ConfigureByConvention();
            });

            builder.Entity<CheckDetail>(b =>
            {
                b.ToTable(TigerConsts.DbTableStockPrefix + "CheckDetail");
                b.ConfigureByConvention();
            });
            builder.Entity<CheckHeader>(b => { 
                b.ToTable(TigerConsts.DbTableStockPrefix + "CheckHeader");
                b.ConfigureByConvention();
            });

            builder.Entity<ReceiptDetail>(b =>
            {
                b.ToTable(TigerConsts.DbTableStockPrefix + "ReceiptDetail");
                b.ConfigureByConvention();
            });
            builder.Entity<ReceiptHeader>(b =>
            {
                b.ToTable(TigerConsts.DbTableStockPrefix + "ReceiptHeader");
                b.ConfigureByConvention();
            });

            builder.Entity<ReverseDetail>(b =>
            {
                b.ToTable(TigerConsts.DbTableStockPrefix + "ReverseDetail");
                b.ConfigureByConvention();
            });
            builder.Entity<ReverseHeader>(b =>
            {
                b.ToTable(TigerConsts.DbTableStockPrefix + "ReverseHeader");
                b.ConfigureByConvention();
            });

            builder.Entity<ShipmentDetail>(b =>
            {
                b.ToTable(TigerConsts.DbTableStockPrefix + "ShipmentDetail");
                b.ConfigureByConvention();
            });
            builder.Entity<ShipmentHeader>(b =>
            {
                b.ToTable(TigerConsts.DbTableStockPrefix + "ShipmentHeader");
                b.ConfigureByConvention();
            });

            builder.Entity<TransferDetail>(b =>
            {
                b.ToTable(TigerConsts.DbTableStockPrefix + "TransferDetail");
                b.ConfigureByConvention();
            });
            builder.Entity<TransferHeader>(b =>
            {
                b.ToTable(TigerConsts.DbTableStockPrefix + "TransferHeader");
                b.ConfigureByConvention();
            });

            #endregion

            #region 营销

            // 会员
            builder.Entity<Coupon>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Coupons",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<CouponHistory>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "CouponHistories",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
            });


            #endregion


            #region 会员模块
            // 会员
            builder.Entity<Member>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Members",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
            });

            // 会员收货地址
            builder.Entity<MemberReceiveAddress>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "MemberReceiveAddresses",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
            });
            #endregion


        }
    }
}