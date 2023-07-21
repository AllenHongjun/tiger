using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.Platform.Versions
{
    /// <summary>
    /// 版本文件
    /// </summary>
    public class VersionFile : AuditedEntity<int>, IMultiTenant
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual Guid? TenantId { get; protected set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public virtual string Path { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 文件版本
        /// </summary>
        public virtual string Version { get; protected set; }

        /// <summary>
        /// 文件大小 单位b
        /// </summary>
        public virtual long Size { get; protected set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public virtual FileType FileType { get; protected set; }

        /// <summary>
        /// 文件SHA256编码
        /// </summary>
        public virtual string SHA256 { get; set; }

        /// <summary>
        /// 下载次数
        /// </summary>
        public virtual int DownloadCount { get; protected set; }

        /// <summary>
        /// 应用版本标识
        /// </summary>
        public virtual Guid AppVersionId { get; protected set; }

        /// <summary>
        /// 所属应用版本号
        /// </summary>
        public virtual AppVersion AppVersion { get; protected set; }
    }
}
