using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Modules.System.Cache
{
    public class GetCacheKeysInput
    {
        public string Prefix { get; set; }

        public string Marker { get; set; }

        public string Filter { get; set; }
    }
}
