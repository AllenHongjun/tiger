using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace Tiger.Module.System.Cache.Permissions
{

    public static class CachingManagementPermissionNames
    {
        public const string GroupName = "AbpCachingManagement";

        public static class Cache
        {
            public const string Default = GroupName + ".Cache";

            public const string Refresh = Default + ".Refresh";

            public const string Delete = Default + ".Delete";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(CachingManagementPermissionNames));
        }
    }
}
