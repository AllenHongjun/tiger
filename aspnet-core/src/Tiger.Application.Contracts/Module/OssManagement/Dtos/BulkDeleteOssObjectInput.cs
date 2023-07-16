using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Module.OssManagement.Dtos
{
    public class BulkDeleteOssObjectInput
    {
        [Required]
        public string Bucket { get; set; }

        public string Path { get; set; }

        [Required]
        public string[] Objects { get; set; }
    }
}
