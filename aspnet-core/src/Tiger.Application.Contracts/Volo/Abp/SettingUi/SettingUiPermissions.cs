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

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(SettingUiPermissions));
        }
    }
}
