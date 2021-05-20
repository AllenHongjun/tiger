using TigerAdmin.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Localization;

namespace TigerAdmin.Permissions
{
    public class TigerAdminPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var identityGroup = context.AddGroup(TigerAdminPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(TigerAdminPermissions.MyPermission1, L("Permission:MyPermission1"));
            var booksPermission = identityGroup.AddPermission(TigerAdminPermissions.Books.Default, L("Permission:Books"));
            booksPermission.AddChild(TigerAdminPermissions.Books.Create, L("Permission:Book.Create"));
            booksPermission.AddChild(TigerAdminPermissions.Books.Edit, L("Permission:Book.Edit"));
            booksPermission.AddChild(TigerAdminPermissions.Books.Delete, L("Permission:Book.Delete"));

            var ouPermission = identityGroup.AddPermission(HelloIdentityPermissions.OrganitaionUnits.Default, IdentityL("Permission:OrganitaionUnitManagement"));
            ouPermission.AddChild(HelloIdentityPermissions.OrganitaionUnits.Create, IdentityL("Permission:Create"));
            ouPermission.AddChild(HelloIdentityPermissions.OrganitaionUnits.Update, IdentityL("Permission:Edit"));
            ouPermission.AddChild(HelloIdentityPermissions.OrganitaionUnits.Delete, IdentityL("Permission:Delete"));

            //var userPermission = identityGroup.GetPermissionOrNull(IdentityPermissions.Users.Default);
            //userPermission?.AddChild(HelloIdentityPermissions.Users.DistributionOrganizationUnit, IdentityL("Permission:DistributionOrganizationUnit"));

            //var rolePermission = identityGroup.GetPermissionOrNull(IdentityPermissions.Roles.Default);
            //rolePermission?.AddChild(HelloIdentityPermissions.Roles.AddOrganizationUnitRole, IdentityL("Permission:AddOrganizationUnitRole"));

            ////Claim
            //var claimPermission = identityGroup.AddPermission(HelloIdentityPermissions.ClaimTypes.Default, IdentityL("Permission:ClaimManagement"));
            //claimPermission.AddChild(HelloIdentityPermissions.ClaimTypes.Create, IdentityL("Permission:Create"));
            //claimPermission.AddChild(HelloIdentityPermissions.ClaimTypes.Update, IdentityL("Permission:Edit"));
            //claimPermission.AddChild(HelloIdentityPermissions.ClaimTypes.Delete, IdentityL("Permission:Delete"));

            ////AuditLogging
            //var auditLogGroup = context.AddGroup(AuditLogPermissions.GroupName);
            //var aduditLogPermission = auditLogGroup.AddPermission(AuditLogPermissions.AuditLogs.Default, AuditLoggingL("Permission:AuditLogManagement"));
            //aduditLogPermission.AddChild(AuditLogPermissions.AuditLogs.Delete, AuditLoggingL("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<TigerAdminResource>(name);
        }

        private static LocalizableString IdentityL(string name)
        {
            return LocalizableString.Create<IdentityResource>(name);
        }
    }
}
