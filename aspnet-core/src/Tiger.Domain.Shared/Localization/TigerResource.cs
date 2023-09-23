using Tiger.Module.System.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Validation.Localization;

namespace Tiger.Localization
{

    /// <summary>
    /// Tiger全局本地化资源
    /// </summary>
    /// <remarks>
    /// 本地化文档 https://docs.abp.io/zh-Hans/abp/latest/Localization
    /// </remarks>
    [InheritResource(typeof(AbpValidationResource))]
    [InheritResource(typeof(AbpLocalizationResource))]
    [LocalizationResourceName("TigerUi")]
    public class TigerResource
    {

    }
}