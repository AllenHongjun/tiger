using Tiger.Business.Orders;
using Tiger.Business.Purchases;
using Tiger.Business.Members;
using Tiger.Business.Stocks;
using Tiger.Stock;
using Tiger.Marketing;
using Tiger.Basic;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Tiger.EntityFrameworkCore
{
    [DependsOn(
        typeof(TigerDomainModule),
        typeof(AbpIdentityEntityFrameworkCoreModule),
        typeof(AbpIdentityServerEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpTenantManagementEntityFrameworkCoreModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule)
        )]
    public class TigerEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            TigerEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<TigerDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
                options.AddRepository<Warehouse, WarehouseRepository>();
                options.AddRepository<Supply, SupplyRepository>();
                options.AddRepository<Store, StoreRepository>();
                options.AddRepository<Sku, SkuRepository>();
                options.AddRepository<FlashPromotion, FlashPromotionRepository>();
                options.AddRepository<FlashPromotionSession, FlashPromotionSessionRepository>();
                options.AddRepository<Inventory, InventoryRepository>();
                options.AddRepository<InventoryHistory, InventoryHistoryRepository>();
                options.AddRepository<CheckHeader, CheckHeaderRepository>();
                options.AddRepository<CheckDetail, CheckDetailRepository>();
                options.AddRepository<ReceiptHeader, ReceiptHeaderRepository>();
                options.AddRepository<ReverseDetail, ReverseDetailRepository>();
                options.AddRepository<ShipmentHeader, ShipmentHeaderRepository>();
                options.AddRepository<ShipmentDetail, ShipmentDetailRepository>();
                options.AddRepository<TransferHeader, TransferHeaderRepository>();
                options.AddRepository<TransferDetail, TransferDetailRepository>();
                options.AddRepository<Member, MemberRepository>();
                options.AddRepository<MemberLevel, MemberLevelRepository>();
                options.AddRepository<MemberLoginLog, MemberLoginLogRepository>();
                options.AddRepository<MemberReceiveAddress, MemberReceiveAddressRepository>();
                options.AddRepository<MemberStatisticInfo, MemberStatisticInfoRepository>();
                options.AddRepository<ReceiptDetail, ReceiptDetailRepository>();
                options.AddRepository<PurchaseHeader, PurchaseHeaderRepository>();
                options.AddRepository<PurchaseReturnDetail, PurchaseReturnDetailRepository>();
                options.AddRepository<PurchaseReturnHeader, PurchaseReturnHeaderRepository>();
                options.AddRepository<PurchaseDetail, PurchaseDetailRepository>();
                options.AddRepository<OrderReturnDetail, OrderReturnDetailRepository>();
                options.AddRepository<OrderReturnHeader, OrderReturnHeaderRepository>();
            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also TigerMigrationsDbContextFactory for EF Core tooling. */
                options.UseSqlServer();
            });
        }
    }
}
