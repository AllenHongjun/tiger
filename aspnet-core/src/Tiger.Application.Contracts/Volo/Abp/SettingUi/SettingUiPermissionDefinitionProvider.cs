using Tiger.Volo.Abp.SettingUi.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Tiger.Volo.Abp.SettingUi
{
    public class SettingUiPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var moduleGroup = context.AddGroup(SettingUiPermissions.GroupName, L("Permission:SettingUi"));
            moduleGroup.AddPermission(SettingUiPermissions.ShowSettingPage, L("Permission:SettingUi.ShowSettingPage"));
            moduleGroup.AddPermission(SettingUiPermissions.Host, L("Permission:SettingUi.Host"), MultiTenancySides.Host);
            moduleGroup.AddPermission(SettingUiPermissions.Tenant, L("Permission:SettingUi.Tenant"));
            moduleGroup.AddPermission(SettingUiPermissions.User, L("Permission:SettingUi.User"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<SettingUiResource>(name);
        }
    }
}
