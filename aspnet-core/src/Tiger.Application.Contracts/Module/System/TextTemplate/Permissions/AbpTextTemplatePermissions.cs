using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace Tiger.Module.System.TextTemplate.Permissions
{
    /// <summary>
    /// 文本模板权限常量
    /// </summary>
    public class AbpTextTemplatePermissions
    {
        public const string GroupName = "AbpTextTemplating";

        public static class TextTemplate
        {
            public const string Default = GroupName + ".TextTemplates";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(AbpTextTemplatePermissions));
        }
    }
}
