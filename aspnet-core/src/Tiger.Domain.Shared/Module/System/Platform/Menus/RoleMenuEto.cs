using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.Platform.Menus
{
    /// <summary>
    /// 角色菜单
    /// </summary>
    [EventName("platform.menus.role_menu")]
    public class RoleMenuEto:IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid MenuId { get; set; }

        public string RoleName { get; set; }
    }
}
