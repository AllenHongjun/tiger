using Tiger.Volo.Abp.Sass.Localization;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.Sass;
public abstract class AbpSaasAppServiceBase : ApplicationService
{
    protected AbpSaasAppServiceBase()
    {
        //ObjectMapperContext = typeof(AbpSaasApplicationModule);
        LocalizationResource = typeof(AbpSaasResource);
    }
}
