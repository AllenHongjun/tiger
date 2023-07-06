using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Modules.System.Cache
{   
    /// <summary>
    /// 缓存刷新输入参数
    /// </summary>
    public class CacheRefreshInput
    {
        public string Key { get; set; }

        public DateTime? AbsoluteExpiration { get; set; }

        public DateTime? SlidingExpiration { get; set; }
    }
}
