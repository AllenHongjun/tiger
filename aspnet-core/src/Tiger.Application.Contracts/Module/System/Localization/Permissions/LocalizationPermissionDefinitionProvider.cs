using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tiger.Module.System.Localization.Permissions
{   
    /// <summary>
    /// 本地化权限定义
    /// </summary>
    public class LocalizationPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var localizationGroup = context.AddGroup(LocalizationPermissions.GroupName, L("Permission:LanguageManagement"));


            var languages = localizationGroup.AddPermission(LocalizationPermissions.Languages.Default, L("Permission:Languages"));
            languages.AddChild(LocalizationPermissions.Languages.Create, L("Permission:Create"));
            languages.AddChild(LocalizationPermissions.Languages.Edit, L("Permission:Edit"));
            languages.AddChild(LocalizationPermissions.Languages.Delete, L("Permission:Delete"));
            languages.AddChild(LocalizationPermissions.Languages.ChangeDefault, L("Permission:LanguagesChangeDefault"));


            var languageTexts = localizationGroup.AddPermission(LocalizationPermissions.LanguageTexts.Default, L("Permission:LanguageTexts"));
            languageTexts.AddChild(LocalizationPermissions.LanguageTexts.Edit, L("Permission:Edit"));
            languageTexts.AddChild(LocalizationPermissions.LanguageTexts.Create, L("Permission:Create"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<LocalizationResource>(name);
        }
    }
}
