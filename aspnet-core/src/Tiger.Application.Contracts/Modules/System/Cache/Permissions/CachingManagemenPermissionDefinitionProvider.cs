using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Modules.System.Cache.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tiger.Modules.System.Cache.Permissions
{
    public class CachingManagemenPermissionDefinitionProvider:
        PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var cachingManagerGroup = context.AddGroup(CachingManagementPermissionNames.GroupName, L("Permission:CachingManagement"));

            var cacheGroup = cachingManagerGroup.AddPermission(CachingManagementPermissionNames.Cache.Default, L("Permission:Caches"));
            cacheGroup.AddChild(CachingManagementPermissionNames.Cache.Refresh, L("Permission:Refresh"));
            cacheGroup.AddChild(CachingManagementPermissionNames.Cache.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CacheResource>(name);
        }
    }
}
