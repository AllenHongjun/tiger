using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.IdentityServer.Localization;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Tiger.Volo.Abp.IdentityServer
{
    public class IdentityServerPermissionDefinitionProvider
        : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var identityServerGroup = context.GetGroupOrNull(IdentityServerPermissions.GroupName);
            if (identityServerGroup == null)
            {
                identityServerGroup = context
                    .AddGroup(
                        name: IdentityServerPermissions.GroupName,
                        displayName: L("Permissions:IdentityServer"),
                        multiTenancySide: MultiTenancySides.Host);
            }
            // 客户端权限
            var clientPermissions = identityServerGroup.AddPermission(
                IdentityServerPermissions.Clients.Default, 
                L("Permissions:Clients"), MultiTenancySides.Host);
            clientPermissions.AddChild(IdentityServerPermissions.Clients.Create, L("Permissions:Create"), MultiTenancySides.Host);
            clientPermissions.AddChild(IdentityServerPermissions.Clients.Update, L("Permissions:Update"), MultiTenancySides.Host);
            clientPermissions.AddChild(IdentityServerPermissions.Clients.Clone, L("Permissions:Clone"), MultiTenancySides.Host);
            clientPermissions.AddChild(IdentityServerPermissions.Clients.Delete, L("Permissions:Delete"), MultiTenancySides.Host);
            clientPermissions.AddChild(IdentityServerPermissions.Clients.ManagePermissions, L("Permissions:ManagePermissions"), MultiTenancySides.Host);
            clientPermissions.AddChild(IdentityServerPermissions.Clients.ManageClaims, L("Permissions:ManageClaims"), MultiTenancySides.Host);
            clientPermissions.AddChild(IdentityServerPermissions.Clients.ManageSecrets, L("Permissions:ManageSecrets"), MultiTenancySides.Host);
            clientPermissions.AddChild(IdentityServerPermissions.Clients.ManageProperties, L("Permissions:ManageProperties"), MultiTenancySides.Host);

            // Api资源权限
            var apiResourcePermissions = identityServerGroup.AddPermission(
                IdentityServerPermissions.ApiResources.Default, 
                L("Permissions:ApiResources"), 
                MultiTenancySides.Host);
            apiResourcePermissions.AddChild(IdentityServerPermissions.ApiResources.Create, L("Permissions:Create"), MultiTenancySides.Host);
            apiResourcePermissions.AddChild(IdentityServerPermissions.ApiResources.Update, L("Permissions:Update"), MultiTenancySides.Host);
            apiResourcePermissions.AddChild(IdentityServerPermissions.ApiResources.Delete, L("Permissions:Delete"), MultiTenancySides.Host);
            apiResourcePermissions.AddChild(IdentityServerPermissions.ApiResources.ManageClaims, L("Permissions:ManageClaims"), MultiTenancySides.Host);
            apiResourcePermissions.AddChild(IdentityServerPermissions.ApiResources.ManageSecrets, L("Permissions:ManageSecrets"), MultiTenancySides.Host);
            apiResourcePermissions.AddChild(IdentityServerPermissions.ApiResources.ManageScopes, L("Permissions:ManageScopes"), MultiTenancySides.Host);
            apiResourcePermissions.AddChild(IdentityServerPermissions.ApiResources.ManageProperties, L("Permissions:ManageProperties"), MultiTenancySides.Host);

            // Api范围权限
            var apiScopePermissions = identityServerGroup.AddPermission(
                IdentityServerPermissions.ApiScopes.Default, 
                L("Permissions:ApiScopes"), 
                MultiTenancySides.Host);
            apiScopePermissions.AddChild(IdentityServerPermissions.ApiScopes.Create, L("Permissions:Create"), MultiTenancySides.Host);
            apiScopePermissions.AddChild(IdentityServerPermissions.ApiScopes.Update, L("Permissions:Update"), MultiTenancySides.Host);
            apiScopePermissions.AddChild(IdentityServerPermissions.ApiScopes.Delete, L("Permissions:Delete"), MultiTenancySides.Host);
            apiScopePermissions.AddChild(IdentityServerPermissions.ApiScopes.ManageClaims, L("Permissions:ManageClaims"), MultiTenancySides.Host);
            apiScopePermissions.AddChild(IdentityServerPermissions.ApiScopes.ManageProperties, L("Permissions:ManageProperties"), MultiTenancySides.Host);

            // 身份资源权限
            var identityResourcePermissions = identityServerGroup.AddPermission(
                IdentityServerPermissions.IdentityResources.Default, 
                L("Permissions:IdentityResources"), 
                MultiTenancySides.Host);
            identityResourcePermissions.AddChild(IdentityServerPermissions.IdentityResources.Create, L("Permissions:Create"), MultiTenancySides.Host);
            identityResourcePermissions.AddChild(IdentityServerPermissions.IdentityResources.Update, L("Permissions:Update"), MultiTenancySides.Host);
            identityResourcePermissions.AddChild(IdentityServerPermissions.IdentityResources.Delete, L("Permissions:Delete"), MultiTenancySides.Host);
            identityResourcePermissions.AddChild(IdentityServerPermissions.IdentityResources.ManageClaims, L("Permissions:ManageClaims"), MultiTenancySides.Host);
            identityResourcePermissions.AddChild(IdentityServerPermissions.IdentityResources.ManageProperties, L("Permissions:ManageProperties"), MultiTenancySides.Host);

            // 持久授权
            var persistedGrantPermissions = identityServerGroup.AddPermission(
                IdentityServerPermissions.Grants.Default, 
                L("Permissions:Grants"), 
                MultiTenancySides.Host);
            persistedGrantPermissions.AddChild(IdentityServerPermissions.Grants.Delete, L("Permissions:Delete"), MultiTenancySides.Host);
            persistedGrantPermissions.AddChild(IdentityServerPermissions.Grants.Create, L("Permissions:Create"), MultiTenancySides.Host);
            persistedGrantPermissions.AddChild(IdentityServerPermissions.Grants.Update, L("Permissions:Update"), MultiTenancySides.Host);



        }


        protected virtual LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpIdentityServerResource>(name);
        }
    }
}
