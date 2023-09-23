using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Tiger.AppSettingDefinition
{
    /// <summary>
    /// Tiger设置
    /// </summary>
    /// <remarks>
    /// 更改依赖模块的设置定义 https://docs.abp.io/zh-Hans/abp/latest/Settings
    /// </remarks>
    public class TigerSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            // 更改默认语言设置的文字
            var defaultLanguage = context.GetOrNull(LocalizationSettingNames.DefaultLanguage);
            if (defaultLanguage != null)
            {
                defaultLanguage.DefaultValue = "zh-Hans";
                defaultLanguage.DisplayName =
                new LocalizableString(
                        typeof(TigerResource),
                        "DisplayName:Abp.Localization.DefaultLanguage"
                    );
                defaultLanguage.Description =
                new LocalizableString(
                        typeof(TigerResource),
                        "Description:Abp.Localization.DefaultLanguage"
                    );
            }
        }
    }

}
