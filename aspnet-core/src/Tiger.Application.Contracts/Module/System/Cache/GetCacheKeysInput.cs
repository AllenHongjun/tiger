using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Cache
{
    public class GetCacheKeysInput
    {
        public string Prefix { get; set; }

        public string Marker { get; set; }

        public string Filter { get; set; }
    }
}
