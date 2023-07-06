using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Cache
{
    public class CackeKeysResponse
    {
        public string NextMarker { get; }

        public IEnumerable<string> Keys { get; }

        public CackeKeysResponse(
            string nextMarker,
            IEnumerable<string> keys)
        {
            NextMarker = nextMarker;
            Keys = keys;
        }
    }
}
