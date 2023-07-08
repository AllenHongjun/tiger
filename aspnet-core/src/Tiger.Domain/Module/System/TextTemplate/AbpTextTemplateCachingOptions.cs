using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.TextTemplate
{
    /// <summary>
    /// 文本模板缓存配置内容
    /// </summary>
    public class AbpTextTemplateCachingOptions
    {
        public AbpTextTemplateCachingOptions()
        {
            MinimumCacheDuration = TimeSpan.FromHours(1);
            MaximumCacheDuration = TimeSpan.FromDays(30);
        }

        /// <summary>
        /// 文本模板缓存最小过期时间
        /// </summary>
        public TimeSpan? MinimumCacheDuration { get; set; } 

        /// <summary>
        /// 文本模板缓存绝对过期时间
        /// </summary>
        public TimeSpan? MaximumCacheDuration { get; set; }
    }
}
