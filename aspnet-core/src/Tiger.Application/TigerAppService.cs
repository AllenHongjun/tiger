using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Localization;
using Volo.Abp.Application.Services;

namespace Tiger
{
    /* Inherit your application services from this class.
     */
    public abstract class TigerAppService : ApplicationService
    {
        protected TigerAppService()
        {
            LocalizationResource = typeof(TigerResource);
        }
    }
}
