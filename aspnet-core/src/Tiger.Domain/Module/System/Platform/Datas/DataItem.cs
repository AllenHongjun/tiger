using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.Platform.Datas
{
    /// <summary>
    /// 数据字典项
    /// </summary>
    public class DataItem : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId {get; set;}

        public virtual string Name { get; set;}

        public virtual string DisplayName { get; set;}

        /// <summary>
        /// 默认值
        /// </summary>
        public virtual string DefaultValue { get; set;}

        /// <summary>
        /// 允许为NULL
        /// </summary>
        public virtual string AllowBeNull { get; set;}

        /// <summary>
        /// 是否静态
        /// </summary>
        public virtual bool IsStatic { get; set;}

        public virtual ValueType ValueType { get; protected set;}

        public virtual Guid DataId { get; protected set; }

        
    }
}
