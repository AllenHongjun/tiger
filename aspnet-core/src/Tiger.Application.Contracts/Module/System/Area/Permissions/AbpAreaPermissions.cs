using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace Tiger.Module.System.Area.Permissions
{
    /// <summary>
    /// 区域权限
    /// </summary>
    public class AbpAreaPermissions
    {
        public const string GroupName = "AbpArea";

        public static class Region
        {
            public const string Default = GroupName + ".Region";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(AbpAreaPermissions));
        }
    }
}
