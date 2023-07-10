using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.Platform.Versions
{
    /// <summary>
    /// 应用版本号
    /// </summary>
    /// <remarks>
    /// 
    /// 类似 https://github.com/zoeminghong/app-version
    /// </remarks>
    public class AppVersion:FullAuditedAggregateRoot<Guid>,IMultiTenant
    {
        public AppVersion()
        {
            Files = new List<VersionFile>();
        }

        public virtual Guid? TenantId { get; protected set; }

        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Title { get; protected set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public virtual string Version { get;protected set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 重要级别
        /// </summary>
        public virtual ImportantLevel Level { get; set; }
        /// <summary>
        /// 适应平台
        /// </summary>
        public virtual PlatformType PlatformType { get; protected set; }
        /// <summary>
        /// 版本文件列表
        /// </summary>
        public virtual ICollection<VersionFile> Files { get; protected set; }
    }
}
