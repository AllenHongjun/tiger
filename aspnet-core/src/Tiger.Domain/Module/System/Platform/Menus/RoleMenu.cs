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
        public RoleMenu(
            Guid id, 
            Guid? tenantId, 
            Guid menuId, 
            string roleName) : base(id)
        {
            TenantId=tenantId;
            MenuId=menuId;
            RoleName=roleName;
        }

        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual Guid? TenantId { get; protected set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public virtual Guid MenuId { get; protected set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public virtual string RoleName { get; protected set; }

        public virtual bool Startup { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { MenuId, RoleName };
        }
    }
}
