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
using Tiger.Business.Marketings;
using Tiger.Business.Purchases;
using Volo.Abp.Identity;

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

            #region Demo模块
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


            // 产品分类 设置字段长度 是否为空 默认值 部分数据库注释
            builder.Entity<Category>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Categorys",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(256).HasComment("分类名称");
                b.Property(x => x.Level).IsRequired().HasMaxLength(10)
                    .HasColumnName(nameof(Category.Level));
                b.Property(x => x.Keyword).HasDefaultValue(false);
                b.Property(x => x.Icon).IsRequired(false).HasMaxLength(512).HasComment("图标地址");
                b.Property(x => x.Keyword).HasMaxLength(256);
                b.Property(x => x.Description).HasMaxLength(512);

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

                b.Property(x => x.Code).IsRequired(true).HasMaxLength(64);
                b.Property(x => x.Name).IsRequired(true).HasMaxLength(64);
                b.Property(x => x.Address).HasMaxLength(256);
                b.Property(x => x.Phone).HasMaxLength(16);
                b.Property(x => x.Apolygons).HasMaxLength(128);
                b.Property(x => x.Lng).HasMaxLength(128);
                b.Property(x => x.Lat).HasMaxLength(128);
            });

            // 供应商
            builder.Entity<Supply>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Supplies",
                    TigerConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Code).IsRequired(true).HasMaxLength(64);
                b.Property(x => x.Name).IsRequired(true).HasMaxLength(64);
                b.Property(x => x.AttentionTo).HasMaxLength(64);
                b.Property(x => x.Phone).HasMaxLength(16);
                b.Property(x => x.PostCode).HasMaxLength(16);
                b.Property(x => x.Province).HasMaxLength(16);
                b.Property(x => x.City).HasMaxLength(16);
                b.Property(x => x.Reginon).HasMaxLength(16);
                b.Property(x => x.Address).HasMaxLength(256);

            });

            // 仓库
            builder.Entity<Warehouse>(b => {
                b.ToTable(TigerConsts.DbTablePrefix + "Warehouses");
                b.ConfigureByConvention();

                b.Property(x => x.Code).IsRequired(true).HasMaxLength(64);
                b.Property(x => x.Name).IsRequired(true).HasMaxLength(64);
                b.Property(x => x.Mobile).HasMaxLength(16);
                b.Property(x => x.PosttalCode).HasMaxLength(16);
                b.Property(x => x.Province).HasMaxLength(16);
                b.Property(x => x.City).HasMaxLength(16);
                b.Property(x => x.District).HasMaxLength(16);
                b.Property(x => x.Address).HasMaxLength(256);
            });




            // 产品属性类型表
            builder.Entity<ProductAttributeType>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "ProductAttributeTypes",
                    TigerConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
                b.HasIndex(x => x.Name);

            });


            // 产品属性表
            builder.Entity<ProductAttribute>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "ProductAttributes",
                    TigerConsts.DbSchema);

                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
                b.Property(x => x.InputList).HasMaxLength(512);
                b.HasIndex(x => x.Name);

            });

            // 产品属性值表
            builder.Entity<ProductAttributeValue>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "ProductAttributeValues",
                    TigerConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Value).HasMaxLength(512);

                //间接多对多关系
                b.HasOne(x => x.Product).WithMany(x => x.ProductAttributeValues).HasForeignKey(x => x.ProductId);
                b.HasOne(x => x.ProductAttribute).WithMany(x => x.ProductAttributeValues).HasForeignKey(x => x.ProductAttributeId);

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


            builder.Entity<ProductAttributeValue>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "ProductAttributeValues", TigerConsts.DbSchema);
                b.ConfigureByConvention();


                /* Configure more properties here */
            });

            #endregion

            #region 采购模块
            //采购单
            builder.Entity<PurchaseHeader>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "PurchaseHeader");

                b.ConfigureByConvention();
                //b.ConfigureFullAuditedAggregateRoot();
                //b.Property(x => x.CreatorId).HasColumnType("");

                b.Property(x => x.Code).IsRequired(true).HasMaxLength(64);
                b.Property(x => x.WarehouseCode).HasMaxLength(64);
                b.Property(x => x.AuditedBy).HasMaxLength(16);
                b.Property(x => x.PurchaseBy).HasMaxLength(16);
                b.Property(x => x.Note).HasMaxLength(512);

            });

            //采购单明细
            builder.Entity<PurchaseDetail>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "PurchaseDetail");

                b.ConfigureByConvention();

                b.Property(x => x.ProductSn).HasMaxLength(64);
                b.Property(x => x.Unit).HasMaxLength(16);
                b.Property(x => x.Note).HasMaxLength(512);
                b.Property(x => x.ProcessStamp).HasMaxLength(16);
            });

            //采购退款单
            builder.Entity<PurchaseReturnHeader>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "PurchaseReturnHeader");

                b.ConfigureByConvention();

                b.Property(x => x.Code).HasMaxLength(32).IsRequired(true);
                b.Property(x => x.Note).HasMaxLength(512);
            });

            //采购退款单明细
            builder.Entity<PurchaseReturnDetail>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "PurchaseReturnDetail");

                b.ConfigureByConvention();

                b.Property(x => x.ProductSn).HasMaxLength(64);
                b.Property(x => x.Unit).HasMaxLength(16);
                b.Property(x => x.Note).HasMaxLength(512);
                b.Property(x => x.ProcessStamp).HasMaxLength(16);
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

                b.Property(x => x.Code).HasMaxLength(32).IsRequired(true);
                b.Property(x => x.DeliveryCompany).HasMaxLength(128);
                b.Property(x => x.DeliverySn).HasMaxLength(32);
                b.Property(x => x.BillHeader).HasMaxLength(32);
                b.Property(x => x.BillContent).HasMaxLength(512);
                b.Property(x => x.BillReceiverPhone).HasMaxLength(16);
                b.Property(x => x.BillReceiverEmail).HasMaxLength(32);

                b.Property(x => x.ReceiverName).HasMaxLength(32);
                b.Property(x => x.ReceiverPhone).HasMaxLength(16);
                b.Property(x => x.ReceiverPostCode).HasMaxLength(16);
                b.Property(x => x.Note).HasMaxLength(512);


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

                b.Property(x => x.OrderCode).HasMaxLength(32);
                b.Property(x => x.ProductPic).HasMaxLength(512);
                b.Property(x => x.ProductName).HasMaxLength(128);
                b.Property(x => x.ProductSn).HasMaxLength(32);
                b.Property(x => x.ProductSkuCode).HasMaxLength(32);
                b.Property(x => x.PromotionAmount).HasMaxLength(32);
                b.Property(x => x.ProductAttr).HasMaxLength(512);
            });

            //订单退款单
            builder.Entity<OrderReturnHeader>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "OrderReturnHeaders",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.Code).HasMaxLength(32);
                b.Property(x => x.Note).HasMaxLength(512);
            });

            ////订单退款明细
            builder.Entity<OrderReturnDetail>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "OrderReturnDetails",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.OrderSn).IsRequired(true).HasMaxLength(32);
                b.Property(x => x.ReturnName).HasMaxLength(16);
                b.Property(x => x.ReturnPhone).HasMaxLength(16);
                b.Property(x => x.ProductPic).HasMaxLength(512);
                b.Property(x => x.ProductName).HasMaxLength(128);
                b.Property(x => x.ProductAttr).HasMaxLength(512);
                b.Property(x => x.Reason).HasMaxLength(512);
                b.Property(x => x.Description).HasMaxLength(512);
                b.Property(x => x.ProofPics).HasMaxLength(512);
                b.Property(x => x.HandleNote).HasMaxLength(512);
                b.Property(x => x.HandleMan).HasMaxLength(16);
                b.Property(x => x.ReceiveMan).HasMaxLength(16);
                b.Property(x => x.ReceiveNote).HasMaxLength(512);



                //b.Property(x => x.ProductSn).HasMaxLength(64);
                //b.Property(x => x.Unit).HasMaxLength(16);
                //b.Property(x => x.Note).HasMaxLength(512);
                //b.Property(x => x.ProcessStamp).HasMaxLength(16);

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

            #region 仓储模块
            builder.Entity<Inventory>(b =>
            {
                b.ToTable(TigerConsts.DbTableStockPrefix + "Inventory");

                b.ConfigureByConvention();

                b.Property(x => x.ProductName).HasMaxLength(128);
                b.Property(x => x.ProductSn).HasMaxLength(32);
                b.Property(x => x.AttributeData).HasMaxLength(512);
            });
            builder.Entity<InventoryHistory>(b =>
            {

                b.ToTable(TigerConsts.DbTableStockPrefix + "InventoryHistory");

                b.ConfigureByConvention();

                b.Property(x => x.ProductName).HasMaxLength(128);
                b.Property(x => x.AttributeData).HasMaxLength(512);
                b.Property(x => x.Batch).HasMaxLength(32);
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

                b.Property(x => x.ProductSn).HasMaxLength(32).IsRequired(true);
                b.Property(x => x.ProductName).HasMaxLength(128);
                b.Property(x => x.Batch).HasMaxLength(32);
                b.Property(x => x.ProcessStamp).HasMaxLength(32);
                
            });
            builder.Entity<CheckHeader>(b => { 
                b.ToTable(TigerConsts.DbTableStockPrefix + "CheckHeader");
                b.ConfigureByConvention();

                b.Property(x => x.WarehouseCode).HasMaxLength(32);

                b.Property(x => x.Code).HasMaxLength(32);

                b.Property(x => x.Note).HasMaxLength(512);

                b.Property(x => x.CloseBy).HasMaxLength(16);

                b.Property(x => x.ProcessStamp).HasMaxLength(32);
            });

            
            builder.Entity<ReceiptHeader>(b =>
            {
                b.ToTable(TigerConsts.DbTableStockPrefix + "ReceiptHeader");
                b.ConfigureByConvention();

                b.Property(x => x.Code).HasMaxLength(32).IsRequired(true);
                b.Property(x => x.Note).HasMaxLength(512);

            });

            builder.Entity<ReceiptDetail>(b =>
            {
                b.ToTable(TigerConsts.DbTableStockPrefix + "ReceiptDetail");
                b.ConfigureByConvention();

                b.Property(x => x.ReceiptCode).HasMaxLength(32);
                b.Property(x => x.WarehouseCode).HasMaxLength(32);
                b.Property(x => x.ProductSn).HasMaxLength(32);
                b.Property(x => x.ProductName).HasMaxLength(32);
                b.Property(x => x.Unit).HasMaxLength(16);
                b.Property(x => x.Batch).HasMaxLength(32);
                b.Property(x => x.ProcessStamp).HasMaxLength(128);
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

                b.Property(x => x.ProductName).HasMaxLength(128);
                b.Property(x => x.ProductSn).HasMaxLength(32);
                b.Property(x => x.Batch).HasMaxLength(32);
                b.Property(x => x.ProcessStamp).HasMaxLength(32);
                b.Property(x => x.Unit).HasMaxLength(16);
            });
            builder.Entity<ShipmentHeader>(b =>
            {
                b.ToTable(TigerConsts.DbTableStockPrefix + "ShipmentHeader");
                b.ConfigureByConvention();

                b.Property(x => x.WarehouseCode).HasMaxLength(32);
                b.Property(x => x.Code).HasMaxLength(32);
                b.Property(x => x.Note).HasMaxLength(512);
            });

            
            builder.Entity<TransferHeader>(b =>
            {
                b.ToTable(TigerConsts.DbTableStockPrefix + "TransferHeader");
                b.ConfigureByConvention();

                b.Property(x => x.Code).HasMaxLength(32);
                b.Property(x => x.CloseBy).HasMaxLength(16);
                b.Property(x => x.Note).HasMaxLength(512);
            });

            builder.Entity<TransferDetail>(b =>
            {
                b.ToTable(TigerConsts.DbTableStockPrefix + "TransferDetail");
                b.ConfigureByConvention();

                b.Property(x => x.TransferCode).HasMaxLength(32);
                b.Property(x => x.ProductSn).HasMaxLength(32);
                b.Property(x => x.ProductName).HasMaxLength(128);
                b.Property(x => x.Unit).HasMaxLength(16);
                b.Property(x => x.ProcessStamp).HasMaxLength(32);
            });

            #endregion

            #region 营销

            // 优惠券
            builder.Entity<Coupon>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "Coupons",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
            });

            // 优惠券使用记录
            builder.Entity<CouponHistory>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "CouponHistories",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasOne(x => x.Order).WithOne(x => x.CouponHistory).OnDelete(DeleteBehavior.Restrict);
            });

            //优惠券关联分类
            builder.Entity<CouponCategoryRelation>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "CouponCategoryRelations",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
            });

            //优惠券关联产品
            builder.Entity<CouponProductRelation>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "CouponProductRelations",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
            });

            //限时购
            builder.Entity<FlashPromotion>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "FlashPromotions",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
            });

            //限时购场次活动
            builder.Entity<FlashPromotionSession>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "FlashPromotionSessions",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
            });

            //限时购活动关联的商品
            builder.Entity<FlashPromotionProductRelation>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "FlashPromotionProductRelations",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasOne(x => x.FlashPromotionSession)
                .WithMany(x => x.FlashPromotionProductRelations)
                .OnDelete(DeleteBehavior.Restrict);
            });

            //限时购通知记录 TODO: 可以改成一个通用的通知 签到通知 。积分提醒通知  降价通知等等 
            builder.Entity<FlashPromotionLog>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "FlashPromotionLogs",
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

            // To Do List: 积分

            // 会员等级
            builder.Entity<MemberLevel>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "MemberLevel",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
            });

            // 会员登录日志
            builder.Entity<MemberLoginLog>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "MemberLoginLog",
                    TigerConsts.DbSchema);
                b.ConfigureByConvention();
            });

            // 会员统计信息
            builder.Entity<MemberStatisticInfo>(b =>
            {
                b.ToTable(TigerConsts.DbTablePrefix + "MemberStatisticInfo",
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
