using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.Localization
{   
    /// <summary>
    /// 语言文本
    /// </summary>
    public class LanguageText:FullAuditedAggregateRoot<Guid>, IMultiTenant
    { 
        /// <summary>
        /// 文化名称
        /// </summary>
        public virtual string CultrueName { get; protected set; }

        /// <summary>
        /// 键名称
        /// </summary>
        public virtual string Key { get; protected set; }

        /// <summary>
        /// 基础值
        /// </summary>
        public virtual string DefaultValue { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public virtual string Value { get; protected set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public virtual string ResourceName { get; protected set; }

        /// <summary>
        /// 租户id
        /// </summary>
        public Guid? TenantId {get; protected set; }

        protected LanguageText()
    {
    }

    public LanguageText(
        Guid id,
        string cultrueName,
        string key,
        string defaultValue,
        string value,
        string resourceName
    ) : base(id)
    {
        CultrueName = cultrueName;
        Key = key;
        DefaultValue = defaultValue;
        Value = value;
        ResourceName = resourceName;
    }
    }
}
