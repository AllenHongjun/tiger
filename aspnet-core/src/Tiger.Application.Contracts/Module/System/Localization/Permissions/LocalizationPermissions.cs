using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace Tiger.Module.System.Localization.Permissions
{   
    /// <summary>
    /// 本地化管理权限常量
    /// </summary>
    public class LocalizationPermissions
    {

        public const string GroupName = "LocalizationManagement";

        public class LanguageTexts
        {
            public const string Default = GroupName +".LanguageTexts";

            public const string Create = GroupName + ".LanguageTexts.Create";

            public const string Update = GroupName + ".LanguageTexts.Update";

            public const string Delete = GroupName + ".LanguageTexts.Delete";
        }

        public class Languages
        {
            public const string Default = GroupName + ".Languages";

            public const string Update = GroupName + ".Languages.Update";

            public const string Create = GroupName + ".Languages.Create";

            public const string ChangeDefault = GroupName + ".Languages.ChangeDefault";

            public const string Delete = GroupName + ".Languages.Delete";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(LocalizationPermissions));
        }
    }
}
