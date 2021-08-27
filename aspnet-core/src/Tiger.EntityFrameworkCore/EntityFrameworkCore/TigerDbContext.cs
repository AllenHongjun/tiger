using Microsoft.EntityFrameworkCore;
using Tiger.Basic;
using Tiger.Books;
using Tiger.Business;
using Tiger.Business.Basic;
using Tiger.Business.Demo;
using Tiger.Business.Orders;
using Tiger.Business.Members;
using Tiger.Marketing;
using Tiger.Orders;
using Tiger.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;
using Tiger.Stock;
using Tiger.Business.Stocks;
using Tiger.Business.Marketings;

namespace Tiger.EntityFrameworkCore
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
    [ConnectionStringName("Default")]
    public class TigerDbContext : AbpDbContext<TigerDbContext>
    {
        public DbSet<AppUser> Users { get; set; }

        //3.EF Core需要你将实体和 DbContext 建立关联.最简单的做法是在Acme.BookStore.EntityFrameworkCore项目的BookStoreDbContext类中添加DbSet属性.如下所示:
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }


        #region Basic
        public DbSet<Basic.Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Sku>  Skus  { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Supply> Sypplies { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<ProductAttributeType> ProductAttributeTypes { get; set; }

        public DbSet<ProductAttribute> ProductAttributes { get; set; }

        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }

        public DbSet<Comment>  Comments { get; set; }

        public DbSet<CommentReply> CommentReplies { get; set; }

        public DbSet<ProductTag> ProductTags { get; set; }

        public DbSet<ProductTagRelation> ProductTagRelations { get; set; }
        #endregion


        #region Order

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<OrderSetting> OrderSettings { get; set; }

        //public DbSet<OrderReturnDetail> OrderReturnDetails { get; set; }

        public DbSet<OrderOperateHistory> OrderOperateHistories { get; set; }
        #endregion

        #region Stock

        public DbSet<Inventory> Inventorys { get; set; }
        public DbSet<InventoryHistory> InventoryHistories { get; set; }

        public DbSet<BomDetail> BomDetails { get; set; }

        public DbSet<BomHeader> BomHeaders { get; set; }

        public DbSet<ReverseDetail> ReverseDetails { get; set; }

        public DbSet<ReverseHeader> ReverseHeaders { get; set; }

        public DbSet<CheckDetail> CheckDetails { get; set; }

        public DbSet<CheckHeader> CheckHeaders { get; set; }

        public DbSet<ReceiptDetail> ReceiptDetails { get; set; }

        public DbSet<ReceiptHeader> ReceiptHeaders { get; set; }


        public DbSet<TransferDetail> TransferDetails { get; set; }

        public DbSet<TransferHeader> TransferHeaders { get; set; }

        public DbSet<ShipmentDetail> ShipmentDetails { get; set; }

        public DbSet<ShipmentHeader> ShipmentHeader { get; set; }

        #endregion



        #region Coupon

        public DbSet<Coupon> Coupons { get; set; }

        public DbSet<CouponHistory> CouponHistories { get; set; }

        public DbSet<CouponProductRelation> CouponProductRelations { get; set; }

        public DbSet<CouponCategoryRelation> CouponCategoryRelations { get; set; }

        public DbSet<FlashPromotion> FlashPromotions { get; set; }

        public DbSet<FlashPromotionSession> FlashPromotionSessions { get; set; }

        public DbSet<FlashPromotionLog> FlashPromotionLogs { get; set; }

        public DbSet<FlashPromotionProductRelation> FlashPromotionProductRelations { get; set; }

        


        #endregion

        #region Member
        public DbSet<Member> Members { get; set; }

        public DbSet<MemberLevel> MemberLevels { get; set; }

        public DbSet<MemberLoginLog> MemberLoginLogs { get; set; }

        public DbSet<MemberReceiveAddress> MemberReceiveAddresses { get; set; }

        public DbSet<MemberStatisticInfo> MemberStatisticInfos { get; set; }




        #endregion


        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside TigerDbContextModelCreatingExtensions.ConfigureTiger
         */

        public TigerDbContext(DbContextOptions<TigerDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
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
            });

            /* Configure your own tables/entities inside the ConfigureTiger method */

            builder.ConfigureTiger();
        }
    }
}
