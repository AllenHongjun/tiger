/**
 * 类    名：TigerPermissionAppService   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/13 9:30:36       
 * 说    明: 
 * 
 */

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.PermissionManagement;

namespace Tiger.Volo.Abp.PermissionManagement
{
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
