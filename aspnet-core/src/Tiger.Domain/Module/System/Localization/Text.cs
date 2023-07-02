using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Tiger.Module.System.Localization
{   
    /// <summary>
    /// 语言文本
    /// </summary>
    public class Text:Entity<int>
    { 
        /// <summary>
        /// 文化名称
        /// </summary>
        public virtual string CultrueName { get; protected set; }

        /// <summary>
        /// 键
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
    }
}
