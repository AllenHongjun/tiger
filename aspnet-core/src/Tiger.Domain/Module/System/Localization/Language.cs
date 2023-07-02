using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Localization;

namespace Tiger.Module.System.Localization
{   
    /// <summary>
    /// 语言
    /// </summary>
    public class Language : AuditedEntity<Guid>, ILanguageInfo
    {   
        /// <summary>
        /// 启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 文化名称
        /// </summary>
        public string CultureName {get; protected set;}

        /// <summary>
        /// 界面文化名称
        /// </summary>
        public string UiCultureName { get; protected set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; protected set; }

        public string FlagIcon { get; protected set; }
    }
}
