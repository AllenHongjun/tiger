using Tiger.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Tiger.Settings
{
    public class TigerSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {

            #region 七牛oss对象存储
            context.Add(
                new SettingDefinition(
                    name: TigerSettings.Qiniu.Oss.AccessKey,
                    defaultValue: "",
                    displayName: L("DisplayName:Qiniu.Oss.AccessKey"),
                    description: L("Description:Qiniu.Oss.AccessKey"),
                    isVisibleToClients: true)
                .WithProviders(
                    DefaultValueSettingValueProvider.ProviderName,
                    ConfigurationSettingValueProvider.ProviderName,
                    GlobalSettingValueProvider.ProviderName,
                    TenantSettingValueProvider.ProviderName),
                new SettingDefinition(
                    name: TigerSettings.Qiniu.Oss.SecretKey,
                    defaultValue: "",
                    displayName: L("DisplayName:Qiniu.Oss.SecretKey"),
                    description: L("Description:Qiniu.Oss.SecretKey"),
                    isVisibleToClients: true)
                .WithProviders(
                    DefaultValueSettingValueProvider.ProviderName,
                    ConfigurationSettingValueProvider.ProviderName,
                    GlobalSettingValueProvider.ProviderName,
                    TenantSettingValueProvider.ProviderName),
                new SettingDefinition(
                    name: TigerSettings.Qiniu.Oss.Bucket,
                    defaultValue: "",
                    displayName: L("DisplayName:Qiniu.Oss.Bucket"),
                    description: L("Description:Qiniu.Oss.Bucket"),
                    isVisibleToClients: true)
                .WithProviders(
                    DefaultValueSettingValueProvider.ProviderName,
                    ConfigurationSettingValueProvider.ProviderName,
                    GlobalSettingValueProvider.ProviderName,
                    TenantSettingValueProvider.ProviderName)
            ); 
            #endregion

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<TigerResource>(name);
        }
    }
}
