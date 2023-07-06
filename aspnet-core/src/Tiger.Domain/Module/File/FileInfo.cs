using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.File
{
    public class FileInfo:AuditedAggregateRoot<Guid>,ISoftDelete,IMultiTenant
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 后缀名
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// Md5值
        /// </summary>
        public string Md5Code { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public decimal Size { get; set; }

        public string Path { get; set; }

        public string Url { get; set; }

        //public FileType Type { get; set; }

        public bool IsDeleted { get; set; } 
    }
}
