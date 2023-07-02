using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.TextTemplate
{   
    /// <summary>
    /// 文本模板
    /// </summary>
    public class TextTemplate:AuditedEntity<Guid>,IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; protected set;}

        /// <summary>
        /// 显示名称
        /// </summary>
        public virtual string DisplayName { get; protected set;}  
        
        /// <summary>
        /// 模板内容
        /// </summary>
        public virtual string Content { get; protected set;}

        /// <summary>
        /// 本地化
        /// </summary>
        public virtual string Culture { get; protected set;}


    }
}
