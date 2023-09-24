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
        public string CultureName { get; protected set; }

        /// <summary>
        /// 界面文化名称
        /// </summary>
        public string UiCultureName { get; protected set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string FlagIcon { get;  set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get;  set; }

        protected Language()
        {
        }

        public Language(
            Guid id,
            bool enable,
            string cultureName,
            string uiCultureName,
            string displayName,
            string flagIcon
        ) : base(id)
        {
            Enable = enable;
            CultureName = cultureName;
            UiCultureName = uiCultureName;
            DisplayName = displayName;
            FlagIcon = flagIcon;
        }

        public void SetDefault(bool isDefault)
        {
            IsDefault = isDefault;
        }
    }
}
