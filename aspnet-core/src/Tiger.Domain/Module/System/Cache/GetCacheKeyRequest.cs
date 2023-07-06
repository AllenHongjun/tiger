using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Cache
{
    public class GetCacheKeyRequest
    {
        public GetCacheKeyRequest(string prefix, string filter, string marker)
        {
            Prefix=prefix;
            Filter=filter;
            Marker=marker;
        }

        public string Prefix { get; }

        public string Filter { get; }

        public string Marker { get; }


    }
}
