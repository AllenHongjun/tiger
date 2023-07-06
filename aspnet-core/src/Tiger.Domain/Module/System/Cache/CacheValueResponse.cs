using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Cache
{
    public class CacheValueResponse
    {
        public CacheValueResponse(
            string type, 
            long size, 
            TimeSpan? ttl, 
            IDictionary<string, object> values)
        {
            Type=type;
            Size=size;
            Ttl=ttl;
            Values=values;
        }

        public string Type { get; }

        public long Size { get; }

        public TimeSpan? Ttl { get; }

        public IDictionary<string, object> Values { get; }


    }
}
