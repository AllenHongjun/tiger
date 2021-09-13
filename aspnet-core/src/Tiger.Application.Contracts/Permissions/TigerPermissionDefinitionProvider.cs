using Tiger.Basic.Products;
using Tiger.Books;
using Tiger.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Localization;

namespace Tiger.Permissions
{
    public class TigerPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            //Define your own permissions here.


            //// 添加 demo 作者权限
            //var bookStoreGroup = context.AddGroup(BookStorePermissions.GroupName);
            //var authorsPermission = bookStoreGroup.AddPermission(
            //        BookStorePermissions.Authors.Default, L("Permission:Authors"));
            //authorsPermission.AddChild(
            //    BookStorePermissions.Authors.Create, L("Permission:Authors.Create"));

            //authorsPermission.AddChild(
            //    BookStorePermissions.Authors.Edit, L("Permission:Authors.Edit"));

            //authorsPermission.AddChild(
            //    BookStorePermissions.Authors.Delete, L("Permission:Authors.Delete"));

            #region 基础


            var basicGroup = context.AddGroup(BasicPermissions.GroupName, L("Permission:Product"));


            // 产品管理权限
            var productsPermission = basicGroup.AddPermission(
                    BasicPermissions.Product.Default, L("Permission:Product"));
            productsPermission.AddChild(
               BasicPermissions.Product.Create, L("Permission:Product.Create"));
            productsPermission.AddChild(
               BasicPermissions.Product.Update, L("Permission:Product.Update"));
            productsPermission.AddChild(
               BasicPermissions.Product.Delete, L("Permission:Product.Delete"));

            // 仓库管理权限
            var warehousePermission = basicGroup.AddPermission(
                    BasicPermissions.Warehouse.Default, L("Permission:Warehouse"));
            warehousePermission.AddChild(
               BasicPermissions.Warehouse.Create, L("Permission:Warehouse.Create"));
            warehousePermission.AddChild(
               BasicPermissions.Warehouse.Update, L("Permission:Warehouse.Update"));
            warehousePermission.AddChild(
               BasicPermissions.Warehouse.Delete, L("Permission:Warehouse.Delete"));

            //供应商管理权限
            var supplyPermission = basicGroup.AddPermission(
                    BasicPermissions.Supply.Default, L("Permission:Supply"));
            supplyPermission.AddChild(
               BasicPermissions.Supply.Create, L("Permission:Supply.Create"));
            supplyPermission.AddChild(
               BasicPermissions.Supply.Update, L("Permission:Supply.Update"));
            supplyPermission.AddChild(
               BasicPermissions.Supply.Delete, L("Permission:Supply.Delete"));

            //店铺管理权限
            var storePermission = basicGroup.AddPermission(
                    BasicPermissions.Store.Default, L("Permission:Store"));
            storePermission.AddChild(
               BasicPermissions.Store.Create, L("Permission:Store.Create"));
            storePermission.AddChild(
               BasicPermissions.Store.Update, L("Permission:Store.Update"));
            storePermission.AddChild(
               BasicPermissions.Store.Delete, L("Permission:Store.Delete"));

            ////SKU管理权限
            //var skuPermission = basicGroup.AddPermission(
            //        BasicPermissions.Sku.Default, L("Permission:Sku"));
            //skuPermission.AddChild(
            //   BasicPermissions.Sku.Create, L("Permission:Sku.Create"));
            //skuPermission.AddChild(
            //   BasicPermissions.Sku.Update, L("Permission:Sku.Update"));
            //skuPermission.AddChild(
            //   BasicPermissions.Sku.Delete, L("Permission:Sku.Delete"));


            // 分类管理权限
            var categorysPermission = basicGroup.AddPermission(
                    BasicPermissions.Category.Default, L("Permission:Category"));
            categorysPermission.AddChild(
                 BasicPermissions.Category.Create, L("Permission:Category.Create"));
            categorysPermission.AddChild(
                 BasicPermissions.Category.Update, L("Permission:Category.Update"));
            categorysPermission.AddChild(
                 BasicPermissions.Category.Delete, L("Permission:Category.Delete"));

            // 产品属性分类权限

            var productAttributeTypesPermission = basicGroup.AddPermission(
                    BasicPermissions.ProductAttributeType.Default, L("Permission:ProductAttributeTypes"));
            productAttributeTypesPermission.AddChild(
                  BasicPermissions.ProductAttributeType.Create, L("Permission:ProductAttributeTypes.Create"));
            productAttributeTypesPermission.AddChild(
                  BasicPermissions.ProductAttributeType.Update, L("Permission:ProductAttributeTypes.Update"));
            productAttributeTypesPermission.AddChild(
                  BasicPermissions.ProductAttributeType.Delete, L("Permission:ProductAttributeTypes.Delete"));


            // 产品属性权限

            var productAttributesPermission = basicGroup.AddPermission(
                    BasicPermissions.ProductAttribute.Default, L("Permission:ProductAttributes"));
            productAttributesPermission.AddChild(
                  BasicPermissions.ProductAttribute.Create, L("Permission:ProductAttributes.Create"));
            productAttributesPermission.AddChild(
                  BasicPermissions.ProductAttribute.Update, L("Permission:ProductAttributes.Update"));
            productAttributesPermission.AddChild(
                  BasicPermissions.ProductAttribute.Delete, L("Permission:ProductAttributes.Delete"));

            // 评论属性权限

            var commentsPermission = basicGroup.AddPermission(
                    BasicPermissions.Comment.Default, L("Permission:Comments"));
            commentsPermission.AddChild(
                  BasicPermissions.Comment.Create, L("Permission:Comments.Create"));
            commentsPermission.AddChild(
                  BasicPermissions.Comment.Update, L("Permission:Comments.Update"));
            commentsPermission.AddChild(
                  BasicPermissions.Comment.Delete, L("Permission:Comments.Delete"));

            #endregion

            #region 采购

            var purchaseGroup = context.AddGroup(PurchasePermissions.GroupName, L("Permission:Purchase"));

            // 采购单管理权限
            var purchasePermission = purchaseGroup.AddPermission(
                    PurchasePermissions.Purchase.Default, L("Permission:Purchase"));
            purchasePermission.AddChild(
               PurchasePermissions.Purchase.Create, L("Permission:Purchase.Create"));
            purchasePermission.AddChild(
               PurchasePermissions.Purchase.Update, L("Permission:CheckHeader.Update"));
            purchasePermission.AddChild(
               PurchasePermissions.Purchase.Delete, L("Permission:Purchase.Delete"));

            // 采购退款单管理权限
            var purchaseReturnPermission = purchaseGroup.AddPermission(
                    PurchasePermissions.PurchaseReturn.Default, L("Permission:PurchaseReturn"));
            purchaseReturnPermission.AddChild(
               PurchasePermissions.PurchaseReturn.Create, L("Permission:PurchaseReturn.Create"));
            purchaseReturnPermission.AddChild(
               PurchasePermissions.PurchaseReturn.Update, L("Permission:PurchaseReturn.Update"));
            purchaseReturnPermission.AddChild(
               PurchasePermissions.PurchaseReturn.Delete, L("Permission:PurchaseReturn.Delete"));

            #endregion

            #region 订单
            // 订单管理权限
            var orderGroup = context.AddGroup(OrderPermissions.GroupName, L("Permission:Order"));

            // 订单
            var orderPermission = orderGroup.AddPermission(
                    OrderPermissions.Order.Default, L("Permission:Order"));
            orderPermission.AddChild(
               OrderPermissions.Order.Create, L("Permission:Order.Create"));
            orderPermission.AddChild(
               OrderPermissions.Order.Update, L("Permission:Order.Update"));
            orderPermission.AddChild(
               OrderPermissions.Order.Delete, L("Permission:Order.Delete"));

            // 退款
            var orderReturnPermission = orderGroup.AddPermission(
                    OrderPermissions.OrderReturn.Default, L("Permission:OrderReturn"));
            orderReturnPermission.AddChild(
               OrderPermissions.OrderReturn.Create, L("Permission:OrderReturn.Create"));
            orderReturnPermission.AddChild(
               OrderPermissions.OrderReturn.Update, L("Permission:OrderReturn.Update"));
            orderReturnPermission.AddChild(
               OrderPermissions.OrderReturn.Delete, L("Permission:OrderReturn.Delete"));

            // 订单设置
            var orderSettingPermission = orderGroup.AddPermission(
                    OrderPermissions.OrderSetting.Default, L("Permission:OrderSetting"));
            orderSettingPermission.AddChild(
               OrderPermissions.OrderSetting.Create, L("Permission:OrderSetting.Create"));
            orderSettingPermission.AddChild(
               OrderPermissions.OrderSetting.Update, L("Permission:OrderSetting.Update"));
            orderSettingPermission.AddChild(
               OrderPermissions.OrderSetting.Delete, L("Permission:OrderSetting.Delete"));
            #endregion

            #region 仓库
            // 仓储权限
            var stockGroup = context.AddGroup(StockPermissions.GroupName, L("Permission:Inventory"));


            // 库存管理权限
            var inventoryPermission = stockGroup.AddPermission(
                    StockPermissions.Inventory.Default, L("Permission:Inventory"));
            inventoryPermission.AddChild(
               StockPermissions.Inventory.Create, L("Permission:Inventory.Create"));
            inventoryPermission.AddChild(
               StockPermissions.Inventory.Update, L("Permission:Inventory.Update"));
            inventoryPermission.AddChild(
               StockPermissions.Inventory.Delete, L("Permission:Inventory.Delete"));

            // 库存记录管理权限
            var inventoryHistoryPermission = stockGroup.AddPermission(
                    StockPermissions.InventoryHistory.Default, L("Permission:InventoryHistory"));
            inventoryHistoryPermission.AddChild(
               StockPermissions.InventoryHistory.Create, L("Permission:InventoryHistory.Create"));
            inventoryHistoryPermission.AddChild(
               StockPermissions.InventoryHistory.Update, L("Permission:InventoryHistory.Update"));
            inventoryHistoryPermission.AddChild(
               StockPermissions.InventoryHistory.Delete, L("Permission:InventoryHistory.Delete"));

            // 盘点单管理权限
            var checkHeaderPermission = stockGroup.AddPermission(
                    StockPermissions.CheckHeader.Default, L("Permission:CheckHeader"));
            checkHeaderPermission.AddChild(
               StockPermissions.CheckHeader.Create, L("Permission:CheckHeader.Create"));
            checkHeaderPermission.AddChild(
               StockPermissions.CheckHeader.Update, L("Permission:CheckHeader.Update"));
            checkHeaderPermission.AddChild(
               StockPermissions.CheckHeader.Delete, L("Permission:CheckHeader.Delete"));

            //// 盘点单明细管理权限
            //var checkDetailPermission = stockGroup.AddPermission(
            //        StockPermissions.CheckDetail.Default, L("Permission:CheckDetail"));
            //checkDetailPermission.AddChild(
            //   StockPermissions.CheckDetail.Create, L("Permission:CheckDetail.Create"));
            //checkDetailPermission.AddChild(
            //   StockPermissions.CheckDetail.Update, L("Permission:CheckDetail.Update"));
            //checkDetailPermission.AddChild(
            //   StockPermissions.CheckDetail.Delete, L("Permission:CheckDetail.Delete"));


            // 入库单管理权限
            var receiptHeaderPermission = stockGroup.AddPermission(
                    StockPermissions.ReceiptHeader.Default, L("Permission:ReceiptHeader"));
            receiptHeaderPermission.AddChild(
               StockPermissions.ReceiptHeader.Create, L("Permission:ReceiptHeader.Create"));
            receiptHeaderPermission.AddChild(
               StockPermissions.ReceiptHeader.Update, L("Permission:ReceiptHeader.Update"));
            receiptHeaderPermission.AddChild(
               StockPermissions.ReceiptHeader.Delete, L("Permission:ReceiptHeader.Delete"));

            // 出库单管理权限
            var shipmentHeaderPermission = stockGroup.AddPermission(
                    StockPermissions.ShipmentHeader.Default, L("Permission:ShipmentHeader"));
            shipmentHeaderPermission.AddChild(
               StockPermissions.ShipmentHeader.Create, L("Permission:ShipmentHeader.Create"));
            shipmentHeaderPermission.AddChild(
               StockPermissions.ShipmentHeader.Update, L("Permission:ShipmentHeader.Update"));
            shipmentHeaderPermission.AddChild(
               StockPermissions.ShipmentHeader.Delete, L("Permission:ShipmentHeader.Delete"));

            // 调拨单管理权限
            var transferHeaderPermission = stockGroup.AddPermission(
                    StockPermissions.TransferHeader.Default, L("Permission:TransferHeader"));
            transferHeaderPermission.AddChild(
               StockPermissions.TransferHeader.Create, L("Permission:TransferHeader.Create"));
            transferHeaderPermission.AddChild(
               StockPermissions.TransferHeader.Update, L("Permission:TransferHeader.Update"));
            transferHeaderPermission.AddChild(
               StockPermissions.TransferHeader.Delete, L("Permission:TransferHeader.Delete"));


            // 拆套单管理权限
            var reverseHeaderPermission = stockGroup.AddPermission(
                    StockPermissions.ReverseHeader.Default, L("Permission:ReverseHeader"));
            reverseHeaderPermission.AddChild(
               StockPermissions.ReverseHeader.Create, L("Permission:ReverseHeader.Create"));
            reverseHeaderPermission.AddChild(
               StockPermissions.ReverseHeader.Update, L("Permission:ReverseHeader.Update"));
            reverseHeaderPermission.AddChild(
               StockPermissions.ReverseHeader.Delete, L("Permission:ReverseHeader.Delete"));


            #endregion

            #region 营销

            // 营销权限
            var marketingGroup = context.AddGroup(MarketingPermissions.GroupName);


            // 优惠券
            var couponPermission = marketingGroup.AddPermission(
                    MarketingPermissions.Coupon.Default, L("Permission:Coupon"));
            couponPermission.AddChild(
               MarketingPermissions.Coupon.Create, L("Permission:Coupon.Create"));
            couponPermission.AddChild(
               MarketingPermissions.Coupon.Update, L("Permission:Coupon.Update"));
            couponPermission.AddChild(
               MarketingPermissions.Coupon.Delete, L("Permission:Coupon.Delete"));

            // 优惠券
            var flashPromotionPermission = marketingGroup.AddPermission(
                    MarketingPermissions.FlashPromotion.Default, L("Permission:FlashPromotion"));
            flashPromotionPermission.AddChild(
               MarketingPermissions.FlashPromotion.Create, L("Permission:Coupon.Create"));
            flashPromotionPermission.AddChild(
               MarketingPermissions.FlashPromotion.Update, L("Permission:Coupon.Update"));
            flashPromotionPermission.AddChild(
               MarketingPermissions.FlashPromotion.Delete, L("Permission:Coupon.Delete"));



            #endregion

            #region 系统
            // 组织
            var identityGroup = context.GetGroup(IdentityPermissions.GroupName);
            var ouPermission = identityGroup.AddPermission(TigerIdentityPermissions.OrganitaionUnits.Default, L("Permission:OrganitaionUnits"));
            ouPermission.AddChild(TigerIdentityPermissions.OrganitaionUnits.Create, L("Permission:OrganitaionUnits.Create"));
            ouPermission.AddChild(TigerIdentityPermissions.OrganitaionUnits.Update, L("Permission:OrganitaionUnits.Edit"));
            ouPermission.AddChild(TigerIdentityPermissions.OrganitaionUnits.Delete, L("Permission:OrganitaionUnits.Delete"));

            //系统日志
            var auditLogGroup = context.AddGroup(AuditLogPermissions.GroupName, L("Permission:System"));
            var aduditLogPermission = auditLogGroup.AddPermission(AuditLogPermissions.AuditLogs.Default, L("Permission:AuditLog"));
            aduditLogPermission.AddChild(AuditLogPermissions.AuditLogs.Create, L("Permission:AuditLog.Create"));
            aduditLogPermission.AddChild(AuditLogPermissions.AuditLogs.Update, L("Permission:AuditLog.Edit"));
            aduditLogPermission.AddChild(AuditLogPermissions.AuditLogs.Delete, L("Permission:AuditLog.Delete"));

            //var userPermission = identityGroup.GetPermissionOrNull(IdentityPermissions.Users.Default);
            //userPermission?.AddChild(TigerIdentityPermissions.Users.DistributionOrganizationUnit, IdentityL("Permission:DistributionOrganizationUnit"));

            //var rolePermission = identityGroup.GetPermissionOrNull(IdentityPermissions.Roles.Default);
            //rolePermission?.AddChild(TigerIdentityPermissions.Roles.AddOrganizationUnitRole, IdentityL("Permission:AddOrganizationUnitRole"));

            ////Claim
            //var claimPermission = identityGroup.AddPermission(TigerIdentityPermissions.ClaimTypes.Default, IdentityL("Permission:ClaimManagement"));
            //claimPermission.AddChild(TigerIdentityPermissions.ClaimTypes.Create, IdentityL("Permission:Create"));
            //claimPermission.AddChild(TigerIdentityPermissions.ClaimTypes.Update, IdentityL("Permission:Edit"));
            //claimPermission.AddChild(TigerIdentityPermissions.ClaimTypes.Delete, IdentityL("Permission:Delete")); 
            #endregion


        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<TigerResource>(name);
        }


    }
}
