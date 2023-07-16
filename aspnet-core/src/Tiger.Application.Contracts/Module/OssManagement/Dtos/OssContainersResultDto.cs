using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.OssManagement.Dtos
{
    public class OssContainersResultDto
    {
        public string Prefix { get; set; }
        public string Marker { get; set; }
        public string NextMarker { get; set; }
        public int MaxKeys { get; set; }
        public List<OssContainerDto> Containers { get; set; } = new List<OssContainerDto>();
    }
}
