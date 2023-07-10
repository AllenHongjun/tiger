using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.Platform.Menus
{
    /// <summary>
    /// 用户菜单
    /// </summary>
    public class UserMenu:AuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid? MenuId { get; protected set; }

        public virtual Guid UserId { get; protected set; }  

        public virtual bool Startup { get; set; }
    }
}
