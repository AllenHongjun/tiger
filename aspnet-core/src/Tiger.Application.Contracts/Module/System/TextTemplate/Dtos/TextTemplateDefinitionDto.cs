using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.TextTemplate.Dtos
{
    /// <summary>
    /// 文本模板定义实体
    /// </summary>
    public class TextTemplateDefinitionDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 展示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 默认文化名称
        /// </summary>
        public string DefaultCultureName { get; set; }

        /// <summary>
        /// 内联本地化
        /// </summary>
        public bool IsInlineLocalized { get; set; }

        /// <summary>
        /// 是否布局页
        /// </summary>
        public bool IsLayout { get; set; }

        /// <summary>
        /// 布局页
        /// </summary>
        public string Layout { get; set; }
    }
}
