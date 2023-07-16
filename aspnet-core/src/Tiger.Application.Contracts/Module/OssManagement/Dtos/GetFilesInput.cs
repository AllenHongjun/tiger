using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.OssManagement.Dtos
{
    public class GetFilesInput : LimitedResultRequestDto
    {
        public string Filter { get; set; }
        public string Path { get; set; }
    }
}
