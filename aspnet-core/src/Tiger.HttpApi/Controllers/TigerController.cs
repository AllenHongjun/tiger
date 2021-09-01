using System;
using Tiger.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class TigerController : AbpController
    {   
        public Guid MemberId = Guid.Parse("ebd06de4-f535-bebb-87d1-39feb3daea6c");

        public Guid SkuId = Guid.NewGuid();

        protected TigerController()
        {
            LocalizationResource = typeof(TigerResource);
        }
    }
}