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
            //var identityGroup = context.AddGroup(TigerPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(TigerPermissions.MyPermission1, L("Permission:MyPermission1"));
            //var booksPermission = identityGroup.AddPermission(TigerPermissions.Books.Default, L("Permission:Books"));
            //booksPermission.AddChild(TigerPermissions.Books.Create, L("Permission:Book.Create"));
            //booksPermission.AddChild(TigerPermissions.Books.Edit, L("Permission:Book.Edit"));
            //booksPermission.AddChild(TigerPermissions.Books.Delete, L("Permission:Book.Delete"));

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


        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<TigerResource>(name);
        }


    }
}
