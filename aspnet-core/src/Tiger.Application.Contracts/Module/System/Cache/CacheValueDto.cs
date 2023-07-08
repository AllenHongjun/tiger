using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Cache
{
    public class CacheValueDto
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        public long Size { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? Expiration { get; set; }

        public IDictionary<string, object> Values { get; set; }
    }
}
