using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
