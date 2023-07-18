using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.System.Platform.Routes;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.Platform.Menus
{
    /// <summary>
    /// 用户收藏的菜单
    /// </summary>
    public class UserFavoriteMenu:AuditedEntity<Guid>, IMultiTenant
    {

        protected UserFavoriteMenu() { }
        public UserFavoriteMenu(
            Guid id,
            Guid menuId,
            Guid userId,
            string framework,
            string name,
            string displayName,
            string path,
            string icon,
            string color,
            string aliasName = null,
            Guid? tenantId = null)
            : base(id)
        {
            MenuId = menuId;
            UserId = userId;
            Framework = Check.NotNullOrWhiteSpace(framework, nameof(framework), LayoutConsts.MaxFrameworkLength);
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), RouteConsts.MaxNameLength);
            DisplayName = Check.NotNullOrWhiteSpace(displayName, nameof(displayName), RouteConsts.MaxDisplayNameLength);
            Path = Check.NotNullOrWhiteSpace(path, nameof(path), RouteConsts.MaxPathLength);
            Icon = Check.Length(icon, nameof(icon), UserFavoriteMenuConsts.MaxIconLength);
            Color = Check.Length(color, nameof(color), UserFavoriteMenuConsts.MaxColorLength);
            AliasName = Check.Length(aliasName, nameof(aliasName), UserFavoriteMenuConsts.MaxAliasNameLength);
            TenantId = tenantId;
        }

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

        public virtual string DisplayName { get; set; }

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
