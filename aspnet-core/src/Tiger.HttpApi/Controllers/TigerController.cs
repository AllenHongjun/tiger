using System;
using Tiger.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class TigerController : AbpController
    {   
        public Guid MemberId = Guid.Parse("3C932116-67C1-97EE-B497-39FE9F94F024");

        public Guid SkuId = Guid.NewGuid();

        protected TigerController()
        {
            LocalizationResource = typeof(TigerResource);
        }
    }
}