using Tiger.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Tiger.Settings
{
    public class TigerSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(TigerSettings.MySetting1));

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

            #region 阿里云对象存储
            //context.Add(
            //        new SettingDefinition(
            //            name: TigerSettings.Aliyun.Oss.AccessKeyId,
            //            defaultValue: "",
            //            displayName: L("DisplayName:Aliyun.Oss.AccessKeyId"),
            //            description: L("Description:Aliyun.Oss.AccessKeyId"),
            //            isVisibleToClients: true)
            //        .WithProviders(
            //            DefaultValueSettingValueProvider.ProviderName,
            //            ConfigurationSettingValueProvider.ProviderName,
            //            GlobalSettingValueProvider.ProviderName,
            //            TenantSettingValueProvider.ProviderName),

            //        new SettingDefinition(
            //            name: TigerSettings.Aliyun.Oss.AccessKeySecret,
            //            defaultValue: "",
            //            displayName: L("DisplayName:Aliyun.Oss.AccessKeySecret"),
            //            description: L("Description:Aliyun.Oss.AccessKeySecret"),
            //            isVisibleToClients: true)
            //        .WithProviders(
            //            DefaultValueSettingValueProvider.ProviderName,
            //            ConfigurationSettingValueProvider.ProviderName,
            //            GlobalSettingValueProvider.ProviderName,
            //            TenantSettingValueProvider.ProviderName),

            //        new SettingDefinition(
            //            name: TigerSettings.Aliyun.Oss.Endpoint,
            //            defaultValue: "",
            //            displayName: L("DisplayName:Aliyun.Oss.Endpoint"),
            //            description: L("Description:Aliyun.Oss.Endpoint"),
            //            isVisibleToClients: true)
            //        .WithProviders(
            //            DefaultValueSettingValueProvider.ProviderName,
            //            ConfigurationSettingValueProvider.ProviderName,
            //            GlobalSettingValueProvider.ProviderName,
            //            TenantSettingValueProvider.ProviderName)
            //        );
            #endregion

            #region 阿里云短信服务
            //context.Add(
            //            new SettingDefinition(
            //                name: TigerSettings.Aliyun.Sms.AccessKeyId,
            //                defaultValue: "",
            //                displayName: L("DisplayName:Aliyun.Sms.AccessKeyId"),
            //                description: L("Description:Aliyun.Sms.AccessKeyId"),
            //                isVisibleToClients: true)
            //            .WithProviders(
            //                DefaultValueSettingValueProvider.ProviderName,
            //                ConfigurationSettingValueProvider.ProviderName,
            //                GlobalSettingValueProvider.ProviderName,
            //                TenantSettingValueProvider.ProviderName),

            //            new SettingDefinition(
            //                name: TigerSettings.Aliyun.Sms.AccessKeySecret,
            //                defaultValue: "",
            //                displayName: L("DisplayName:Aliyun.Sms.AccessKeySecret"),
            //                description: L("Description:Aliyun.Sms.AccessKeySecret"),
            //                isVisibleToClients: true)
            //            .WithProviders(
            //                DefaultValueSettingValueProvider.ProviderName,
            //                ConfigurationSettingValueProvider.ProviderName,
            //                GlobalSettingValueProvider.ProviderName,
            //                TenantSettingValueProvider.ProviderName),

            //            new SettingDefinition(
            //                name: TigerSettings.Aliyun.Sms.Endpoint,
            //                defaultValue: "",
            //                displayName: L("DisplayName:Aliyun.Sms.Endpoint"),
            //                description: L("Description:Aliyun.Sms.Endpoint"),
            //                isVisibleToClients: true)
            //            .WithProviders(
            //                DefaultValueSettingValueProvider.ProviderName,
            //                ConfigurationSettingValueProvider.ProviderName,
            //                GlobalSettingValueProvider.ProviderName,
            //                TenantSettingValueProvider.ProviderName)
            //            ); 
            #endregion



        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<TigerResource>(name);
        }
    }
}
