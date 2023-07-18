using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.Platform.Menus
{
    /// <summary>
    /// 用户菜单
    /// </summary>
    public class UserMenuEto:IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid MenuId { get; set; }

        public Guid UserId { get; set; }
    }
}
