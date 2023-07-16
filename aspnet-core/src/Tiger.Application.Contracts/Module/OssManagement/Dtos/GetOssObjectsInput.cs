using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.OssManagement.Dtos
{
    public class GetOssObjectsInput : PagedAndSortedResultRequestDto
    {
        public string Bucket { get; set; }
        public string Prefix { get; set; }
        public string Delimiter { get; set; }
        public string Marker { get; set; }
        public string EncodingType { get; set; }
        public bool MD5 { get; set; }
    }
}
