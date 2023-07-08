using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Cache
{
    public class CacheKeysDto
    {
        public string NextMarker { get; set; }

        public List<string> Keys { get; set; } = new List<string>();
    }
}
