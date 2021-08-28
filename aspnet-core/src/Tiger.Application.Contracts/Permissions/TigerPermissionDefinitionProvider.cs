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
            

            // 添加 demo 作者权限
            var bookStoreGroup = context.AddGroup(BookStorePermissions.GroupName);
            var authorsPermission = bookStoreGroup.AddPermission(
                    BookStorePermissions.Authors.Default, L("Permission:Authors"));
                                    authorsPermission.AddChild(
                                        BookStorePermissions.Authors.Create, L("Permission:Authors.Create"));

                                    authorsPermission.AddChild(
                                        BookStorePermissions.Authors.Edit, L("Permission:Authors.Edit"));

                                    authorsPermission.AddChild(
                                        BookStorePermissions.Authors.Delete, L("Permission:Authors.Delete"));

            #region 基础

            // 产品管理权限
            var basicGroup = context.AddGroup(BasicPermissions.GroupName);



            var productsPermission = basicGroup.AddPermission(
                    BasicPermissions.Product.Default, L("Permission:Product"));
            productsPermission.AddChild(
               BasicPermissions.Product.Create, L("Permission:Product.Create"));
            productsPermission.AddChild(
               BasicPermissions.Product.Update, L("Permission:Product.Update"));
            productsPermission.AddChild(
               BasicPermissions.Product.Delete, L("Permission:Product.Delete"));

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

            #endregion

            #region 订单
            // 订单管理权限
            var orderGroup = context.AddGroup(OrderPermissions.GroupName);

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
            //// 营销权限
            //var marketingGroup = context.AddGroup(MarketingPermissions.GroupName);


            //// 优惠券
            //var couponPermission = marketingGroup.AddPermission(
            //        MarketingPermissions.Coupon.Default, L("Permission:Coupon"));
            //couponPermission.AddChild(
            //   MarketingPermissions.Coupon.Create, L("Permission:Coupon.Create"));
            //couponPermission.AddChild(
            //   MarketingPermissions.Coupon.Update, L("Permission:Coupon.Update"));
            //couponPermission.AddChild(
            //   MarketingPermissions.Coupon.Delete, L("Permission:Coupon.Delete"));


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
            var auditLogGroup = context.AddGroup(AuditLogPermissions.GroupName);
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
