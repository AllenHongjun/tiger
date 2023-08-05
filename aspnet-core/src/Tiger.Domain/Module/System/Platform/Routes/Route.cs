using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.Platform.Routes
{
    /// <summary>
    /// 路由实体
    /// </summary>
    /// <remarks>
    /// 这是基于 Vue Router 的路由规则设计的实体,详情:https://router.vuejs.org/zh/api/#routes
    /// </remarks>
    public abstract class Route : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户id
        /// </summary>
        public Guid? TenantId {get; protected set;}
         
        /// <summary>
        /// 路径
        /// </summary>
        public virtual string Path { get;set; }
         
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public virtual string DisplayName { get; set; }
         
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 重定向路径
        /// </summary>
        public virtual string Redirect { get; set; }

        /// <summary>
        /// 状态 启用；禁用
        /// </summary>
        public virtual bool Status { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public virtual string Icon { get; set; }

        protected Route() { }

        protected Route(
            [NotNull] Guid id,
            [NotNull] string path,
            [NotNull] string name,
            [NotNull] string displayName,
            [CanBeNull] string redirect = "",
            [CanBeNull] string description = "",
            [NotNull] bool status = true,
            [CanBeNull] string icon = "",
            [CanBeNull] Guid? tenantId = null)
            : base(id)
        {
            Check.NotNullOrWhiteSpace(path, nameof(path));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(displayName, nameof(displayName));

            Path = path;
            Name = name;
            DisplayName = displayName;
            Redirect = redirect;
            Description = description;
            Status = status;
            Icon = icon;
            TenantId = tenantId;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is Route route)
            {
                return route.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase);
            }
            return base.Equals(obj);
        }
    }
}
