using Tiger.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class TigerController : AbpController
    {
        protected TigerController()
        {
            LocalizationResource = typeof(TigerResource);
        }
    }
}