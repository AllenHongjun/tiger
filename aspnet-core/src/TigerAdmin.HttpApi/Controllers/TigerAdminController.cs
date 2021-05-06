using TigerAdmin.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace TigerAdmin.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class TigerAdminController : AbpController
    {
        protected TigerAdminController()
        {
            LocalizationResource = typeof(TigerAdminResource);
        }
    }
}