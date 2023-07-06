using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Module.System.Cache
{
    public class RefreshCacheRequest
    {
        public RefreshCacheRequest(string key, TimeSpan? absoluteExpiration, TimeSpan? slidingExpiration)
        {
            Key=key;
            AbsoluteExpiration=absoluteExpiration;
            SlidingExpiration=slidingExpiration;
        }

        [Required]
        public string Key { get; }

        public TimeSpan? AbsoluteExpiration { get; }

        public TimeSpan? SlidingExpiration { get; }


    }
}
