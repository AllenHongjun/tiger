using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.TextTemplate
{
    /// <summary>
    /// 文本模板
    /// </summary>
    /// <remarks>
    /// 结合ABP的 模板模块开发的模板管理功能
    /// https://docs.abp.io/en/abp/latest/Text-Templating-Razor
    /// </remarks>
    public class TextTemplate : AuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; protected set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public virtual string DisplayName { get; protected set; }

        /// <summary>
        /// 模板内容
        /// </summary>
        public virtual string Content { get; protected set; }

        /// <summary>
        /// 本地化
        /// </summary>
        public virtual string Culture { get; protected set; }



        protected TextTemplate()
        {
        }

        public TextTemplate(
            Guid id,
            string name,
            string displayName,
            string content,
            string culture
        ) : base(id)
        {
            Name = name;
            DisplayName = displayName;
            Content = content;
            Culture = culture;
        }

        public void SetContent( string content)
        {
            Content = Check.NotNullOrEmpty(content, nameof(content),TextTemplateConsts.MaxContentLength);
        }
    }
}
