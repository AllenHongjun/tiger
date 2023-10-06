using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace Tiger.Volo.Abp.SettingUi
{
    public class SettingUiPermissions
    {
        public const string GroupName = "SettingUi";
        public const string ShowSettingPage = GroupName + ".ShowSettingPage";
        public const string Host = GroupName + ".Host";
        public const string Tenant = GroupName + ".Tenant";
        public const string User = GroupName + ".User";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(SettingUiPermissions));
        }
    }
}
