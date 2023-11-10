using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Train
{
    /// <summary>
    /// 知识库
    /// </summary>
    public class Repository:FullAuditedEntity<Guid>,IMultiTenant
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 类型ID
        /// </summary>
        public Guid RepositoryTypeId { get; set; }

        ///// <summary>
        ///// 资源Id 1.音频 2.视频
        ///// </summary>
        //public int ResourceID { get; set; }

        ///// <summary>
        ///// 类型 
        ///// </summary>
        //public int TypeID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 附件地址
        /// </summary>
        public string FileUrl { get; set; }
        
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        public virtual RepositoryType RepositoryType { get; set; }
        
    }
}
