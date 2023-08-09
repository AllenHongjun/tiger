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
        protected IPermissionChecker PermissionChecker { get; }

        // TODO: LazyGetRequiredService 获取注入方法测试使用

        protected AbpIdentityServerAppServiceBase(
            IPermissionChecker permissionChecker)
        {
            LocalizationResource = typeof(AbpIdentityServerResource);
            PermissionChecker = permissionChecker;
        }

        protected async virtual Task<bool> IsGrantAsync(string policy)
        {
            return await PermissionChecker.IsGrantedAsync(policy);
        }
    }
}
