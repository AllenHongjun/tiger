using System;
using System.Collections.Generic;
using System.Text;
using TigerAdmin.Localization;
using Volo.Abp.Application.Services;

namespace TigerAdmin
{
    /* Inherit your application services from this class.
     */
    public abstract class TigerAdminAppService : ApplicationService
    {
        protected TigerAdminAppService()
        {
            LocalizationResource = typeof(TigerAdminResource);
        }
    }
}
