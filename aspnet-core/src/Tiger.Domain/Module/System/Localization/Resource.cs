using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Module.System.Localization
{   
    /// <summary>
    /// 资源
    /// </summary>
    public class Resource:AuditedEntity<Guid>
    {   
        /// <summary>
        /// 启用
        /// </summary>
        public virtual bool Enable { get;set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public virtual bool Name { get;set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public virtual string DisplayName { get;set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get;set; }



    protected Resource()
    {
    }

    public Resource(
        Guid id,
        bool enable,
        bool name,
        string displayName,
        string description
    ) : base(id)
    {
        Enable = enable;
        Name = name;
        DisplayName = displayName;
        Description = description;
    }
    }
}
