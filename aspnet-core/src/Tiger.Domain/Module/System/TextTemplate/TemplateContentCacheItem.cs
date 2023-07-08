using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.TextTemplate
{
    /// <summary>
    /// 文本模板内容缓存
    /// </summary>
    public class TemplateContentCacheItem
    {
        private const string CacheKeyFormat = "pn:template-content,n:{0},c:{1}";

        public TemplateContentCacheItem()
        {
        }

        public TemplateContentCacheItem(string name, string culture, string content = null)
        {
            Name=name;
            Culture=culture;
            Content=content;
        }

        public string Name { get; set; }

        public string Culture { get; set; }

        public string Content { get; set; }

        public static string CalculateCacheKey(
            string name,
            string culture = null)
        {
            return string.Format(CacheKeyFormat, name, culture);
        }
    }
}
