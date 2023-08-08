using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.IdentityServer.Localization;
using Volo.Abp.Localization;

namespace Tiger.Volo.Abp.IdentityServer
{
    public abstract class AbpIdentityServerAppServiceBase : ApplicationService
    {
        //protected IPermissionChecker PermissionChecker => ServiceProvider.GetService(Type IPermissionChecker);
        protected AbpIdentityServerAppServiceBase()
        {
            LocalizationResource = typeof(AbpIdentityServerResource);
        }

        protected async virtual Task<bool> IsGrantAsync(string policy)
        {   
            throw new NotImplementedException();
            //return await PermissionChecker.IsGrantedAsync(policy);
        }
    }
}
