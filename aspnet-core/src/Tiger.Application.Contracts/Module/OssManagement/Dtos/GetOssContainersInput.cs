using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.OssManagement.Dtos
{
    public class GetOssContainersInput : PagedAndSortedResultRequestDto
    {
        public string Prefix { get; set; }
        public string Marker { get; set; }
    }
}
