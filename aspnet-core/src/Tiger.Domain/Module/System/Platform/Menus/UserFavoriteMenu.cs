using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.Platform.Menus
{
    /// <summary>
    /// 用户收藏的菜单
    /// </summary>
    public class UserFavoriteMenu:AuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid MenuId { get; protected set; }

        public virtual Guid UserId { get; protected set; }

        /// <summary>
        /// 别名
        /// </summary>
        public virtual string AliasName { get;set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public virtual string Color { get; set; }

        /// <summary>
        /// 框架
        /// </summary>
        public virtual string Framework { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public virtual string Path { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public virtual string Icon { get; set; }
    }
}
