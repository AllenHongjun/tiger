using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace Tiger.Volo.Abp.Sass.Permissions
{   
    /// <summary>
    /// 权限定义
    /// </summary>
    public static class AbpSaasPermissions
    {
        public const string GroupName = "AbpSaasPermissions";

        public static class Editions
        {
            public const string Default = GroupName + ".Editions";

            public const string Create = Default + ".Create";

            public const string Update = Default + ".Update";

            public const string Delete = Default + ".Delete";

            public const string ManageFeatures = Default + ".ManageFeatures";
        }

        public static class Tenants
        {
            public const string Default = GroupName + ".Tenants";

            public const string Create = Default + ".Create";

            public const string Update = Default + ".Update";

            public const string Delete = Default + ".Delete";

            /// <summary>
            /// 管理功能
            /// </summary>
            public const string ManageFeatures = Default + ".ManageFeatures";

            /// <summary>
            /// 关联连接字符串
            /// </summary>
            public const string ManageConnectionStrings = Default + ".ManageConnectionStrings";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(AbpSaasPermissions));
        }
    }
}
