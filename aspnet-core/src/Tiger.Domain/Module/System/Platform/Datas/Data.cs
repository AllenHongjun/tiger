using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.Platform.Datas
{
    /// <summary>
    /// 数据字典
    /// </summary>
    public class Data : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId {get; set;}   

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set;} 

        /// <summary>
        /// 编码
        /// </summary>
        public virtual string Code { get; set;}

        /// <summary>
        /// 显示名称
        /// </summary>
        public virtual string DisplayName { get; set;}

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set;}

        /// <summary>
        /// 父级ID
        /// </summary>
        public virtual Guid? ParentId { get; set;}

        /// <summary>
        /// 是否静态
        /// </summary>
        public virtual bool IsStatic { get; set;}   

        public virtual ICollection<DataItem> Items { get; protected set;}

        protected Data()
        {
            Items = new Collection<DataItem>();
        }
    }
}
