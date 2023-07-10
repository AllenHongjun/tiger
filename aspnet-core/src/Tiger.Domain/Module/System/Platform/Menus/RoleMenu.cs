using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.Platform.Menus
{
    /// <summary>
    /// 角色菜单
    /// </summary>
    public class RoleMenu:AuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid MenuId { get; protected set; }

        public virtual string RoleName { get; protected set; }

        public virtual bool Startup { get; set; }
    }
}
