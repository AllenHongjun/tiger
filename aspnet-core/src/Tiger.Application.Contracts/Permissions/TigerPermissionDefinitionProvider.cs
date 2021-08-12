using Tiger.Localization;
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

            var identityGroup = context.GetGroup(IdentityPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(TigerPermissions.MyPermission1, L("Permission:MyPermission1"));
            //var booksPermission = identityGroup.AddPermission(TigerPermissions.Books.Default, L("Permission:Books"));
            //booksPermission.AddChild(TigerPermissions.Books.Create, L("Permission:Book.Create"));
            //booksPermission.AddChild(TigerPermissions.Books.Edit, L("Permission:Book.Edit"));
            //booksPermission.AddChild(TigerPermissions.Books.Delete, L("Permission:Book.Delete"));

            var ouPermission = identityGroup.AddPermission(TigerIdentityPermissions.OrganitaionUnits.Default, L("Permission:OrganitaionUnitManagement"));
            ouPermission.AddChild(TigerIdentityPermissions.OrganitaionUnits.Create, L("Permission:Create"));
            ouPermission.AddChild(TigerIdentityPermissions.OrganitaionUnits.Update, L("Permission:Edit"));
            ouPermission.AddChild(TigerIdentityPermissions.OrganitaionUnits.Delete, L("Permission:Delete"));

            //var userPermission = identityGroup.GetPermissionOrNull(IdentityPermissions.Users.Default);
            //userPermission?.AddChild(TigerIdentityPermissions.Users.DistributionOrganizationUnit, IdentityL("Permission:DistributionOrganizationUnit"));

            //var rolePermission = identityGroup.GetPermissionOrNull(IdentityPermissions.Roles.Default);
            //rolePermission?.AddChild(TigerIdentityPermissions.Roles.AddOrganizationUnitRole, IdentityL("Permission:AddOrganizationUnitRole"));

            ////Claim
            //var claimPermission = identityGroup.AddPermission(TigerIdentityPermissions.ClaimTypes.Default, IdentityL("Permission:ClaimManagement"));
            //claimPermission.AddChild(TigerIdentityPermissions.ClaimTypes.Create, IdentityL("Permission:Create"));
            //claimPermission.AddChild(TigerIdentityPermissions.ClaimTypes.Update, IdentityL("Permission:Edit"));
            //claimPermission.AddChild(TigerIdentityPermissions.ClaimTypes.Delete, IdentityL("Permission:Delete"));

            ////AuditLogging
            //var auditLogGroup = context.AddGroup(AuditLogPermissions.GroupName);
            //var aduditLogPermission = auditLogGroup.AddPermission(AuditLogPermissions.AuditLogs.Default, AuditLoggingL("Permission:AuditLogManagement"));
            //aduditLogPermission.AddChild(AuditLogPermissions.AuditLogs.Delete, AuditLoggingL("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<TigerResource>(name);
        }

        private static LocalizableString IdentityL(string name)
        {
            return LocalizableString.Create<IdentityResource>(name);
        }
    }
}
