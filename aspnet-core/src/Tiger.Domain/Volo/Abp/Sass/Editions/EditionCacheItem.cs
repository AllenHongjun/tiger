using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.MultiTenancy;

namespace Tiger.Volo.Abp.Sass.Editions
{
    [Serializable]
    [IgnoreMultiTenancy]
    public class EditionCacheItem
    {
        private const string CacheKeyFormat = "t:{0}";

        public EditionInfo Value { get; set; }

        public EditionCacheItem() { }

        public EditionCacheItem(EditionInfo value)
        {
            Value=value;
        }

        public static string CalculateCacheKey(Guid tenantId)
        {
            return string.Format(CacheKeyFormat, tenantId.ToString());
        }
    }
}
