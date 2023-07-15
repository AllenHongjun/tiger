using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.OssManagement.Dto
{
    public class GetOssContainersResponse
    {
        public string Prefix { get; }
        public string Marker { get; }
        public string NextMarker { get; }
        public int MaxKeys { get; }
        public List<OssContainer> Containers { get; }

        public GetOssContainersResponse(
            string prefix,
            string marker,
            string nextMarker,
            int maxKeys,
            List<OssContainer> containers)
        {
            Prefix = prefix;
            Marker = marker;
            NextMarker = nextMarker;
            MaxKeys = maxKeys;

            Containers = containers;
        }
    }
}
