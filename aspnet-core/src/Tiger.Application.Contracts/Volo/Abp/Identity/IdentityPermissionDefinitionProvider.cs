using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Tiger.Volo.Abp.Identity
{   
    /// <summary>
    /// 身份认证模块权限定义
    /// </summary>
    public class IdentityPermissionDefinitionProvider : PermissionDefinitionProvider
    {

        public override void Define(IPermissionDefinitionContext context)
        {
            var identityGroup = context.GetGroupOrNull(IdentityPermissions.GroupName);
            if (identityGroup != null)
            {   
                //用户权限
                var userPermission = identityGroup.GetPermissionOrNull(IdentityPermissions.Users.Default);
                if (userPermission != null)
                {
                    userPermission.AddChild(TigerIdentityPermissions.Users.ResetPassword, L("Permission:ResetPassword"));
                    userPermission.AddChild(TigerIdentityPermissions.Users.ManageClaims, L("Permission:ManageClaims"));
                    userPermission.AddChild(TigerIdentityPermissions.Users.ManageOrganizationUnits, L("Permission:ManageOrganizationUnits"));
                }

                //角色权限
                var rolePermission = identityGroup.GetPermissionOrNull(IdentityPermissions.Roles.Default);
                if (rolePermission != null)
                {
                    rolePermission.AddChild(TigerIdentityPermissions.Roles.ManageClaims, L("Permission:ManageClaims"));
                    rolePermission.AddChild(TigerIdentityPermissions.Roles.ManageOrganizationUnits, L("Permission:ManageOrganizationUnits"));
                }

                //组织机构权限
                var origanizationUnitPermission = identityGroup.AddPermission(TigerIdentityPermissions.OrganizationUnits.Default, L("Permission:OrganizationUnitManagement"));
                    origanizationUnitPermission.AddChild(TigerIdentityPermissions.OrganizationUnits.Create, L("Permission:Create"));
                    origanizationUnitPermission.AddChild(TigerIdentityPermissions.OrganizationUnits.Update, L("Permission:Update"));
                    origanizationUnitPermission.AddChild(TigerIdentityPermissions.OrganizationUnits.Delete, L("Permission:Delete"));
                    origanizationUnitPermission.AddChild(TigerIdentityPermissions.OrganizationUnits.ManageRoles, L("Permission:ManageRoles"));
                    origanizationUnitPermission.AddChild(TigerIdentityPermissions.OrganizationUnits.ManageUsers, L("Permission:ManageUsers"));
                    origanizationUnitPermission.AddChild(TigerIdentityPermissions.OrganizationUnits.ManagePermissions, L("Permission:ChangePermissions"));

                // 2020-10-23 修复Bug 租户用户也必须能查询自定义的声明, 管理权限只能为主机
                var identityClaimType = identityGroup.AddPermission(TigerIdentityPermissions.IdentityClaimType.Default, L("Permission:IdentityClaimTypeManagement"));
                    identityClaimType.AddChild(TigerIdentityPermissions.IdentityClaimType.Create, L("Permission:Create"), MultiTenancySides.Host);
                    identityClaimType.AddChild(TigerIdentityPermissions.IdentityClaimType.Update, L("Permission:Update"), MultiTenancySides.Host);
                    identityClaimType.AddChild(TigerIdentityPermissions.IdentityClaimType.Delete, L("Permission:Delete"), MultiTenancySides.Host);

                // 定义安全日志管理权限
                var identitySecurityLog = identityGroup.AddPermission(TigerIdentityPermissions.IdentitySecurityLog.Default, L("Permission:IdentitySecurityLogManagement"));
                    identitySecurityLog.AddChild(TigerIdentityPermissions.IdentitySecurityLog.Delete, L("Permission:Delete"), MultiTenancySides.Host);
            }
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<IdentityResource>(name);
        }
    }
}
