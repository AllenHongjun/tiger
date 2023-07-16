using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.OssManagement.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Tiger.Module.OssManagement.Settings
{   
    /// <summary>
    /// OSS管理设置值定义
    /// </summary>
    public class AbpOssManagementSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(CreateFileSystemSettings());
        }

        protected SettingDefinition[] CreateFileSystemSettings()
        {
            return new SettingDefinition[]
            {
                new SettingDefinition(
                    name: AbpOssManagementSettingNames.FileLimitLength,
                    defaultValue: AbpOssManagementSettingNames.DefaultFileLimitLength.ToString(),
                    displayName: L("DisplayName:FileLimitLength"),
                    description: L("Description:FileLimitLength"),
                    isVisibleToClients: true)
                .WithProviders(
                    DefaultValueSettingValueProvider.ProviderName,
                    ConfigurationSettingValueProvider.ProviderName,
                    GlobalSettingValueProvider.ProviderName,
                    TenantSettingValueProvider.ProviderName),
                new SettingDefinition(
                    name: AbpOssManagementSettingNames.AllowFileExtensions,
                    defaultValue: AbpOssManagementSettingNames.DefaultAllowFileExtensions,
                    displayName: L("DisplayName:AllowFileExtensions"),
                    description: L("Description:AllowFileExtensions"),
                    isVisibleToClients: true)
                .WithProviders(
                    DefaultValueSettingValueProvider.ProviderName,
                    ConfigurationSettingValueProvider.ProviderName,
                    GlobalSettingValueProvider.ProviderName,
                    TenantSettingValueProvider.ProviderName),
            };
        }

        protected LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpOssManagementResource>(name);
        }
    }
}
