using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Volo.Abp.Sass.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Volo.Abp.Sass
{
    public abstract class AbpSaasControllerBase : AbpController
    {
        protected AbpSaasControllerBase()
        {
            LocalizationResource = typeof(AbpSaasResource);
        }
    }
}
