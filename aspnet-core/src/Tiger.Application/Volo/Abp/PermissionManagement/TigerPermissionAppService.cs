using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.PermissionManagement;

namespace Tiger.Volo.Abp.PermissionManagement
{
    [RemoteService(false)]
    public class TigerPermissionAppService : PermissionAppService
    {
        public TigerPermissionAppService(
            IPermissionManager permissionManager, 
            IPermissionDefinitionManager permissionDefinitionManager, 
            IOptions<PermissionManagementOptions> options
            ) : base(permissionManager, permissionDefinitionManager, options)
        {

        }
    }
}
