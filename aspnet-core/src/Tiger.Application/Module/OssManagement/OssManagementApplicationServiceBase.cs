using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.OssManagement.Localization;
using Volo.Abp.Application.Services;
using Volo.Abp.Localization;

namespace Tiger.Module.OssManagement
{
    public abstract class OssManagementApplicationServiceBase : ApplicationService
    {
        protected OssManagementApplicationServiceBase()
        {
            LocalizationResource = typeof(AbpOssManagementResource);
        }
    }
}
