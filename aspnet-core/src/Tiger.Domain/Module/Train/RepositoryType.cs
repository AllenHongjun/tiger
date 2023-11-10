using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Train
{
    /// <summary>
    /// 知识库类型
    /// </summary>
    public class RepositoryType:FullAuditedEntity<Guid>,IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public virtual int Name { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool Enable { get; set; }
        
        /// <summary>
        /// 顺序
        /// </summary>
        public int Sort { get; set; }

        public virtual ICollection<Repository> Repositories { get; set; }
    }
}
