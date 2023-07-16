using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Module.OssManagement.Dtos
{
    public class GetOssObjectInput
    {
        [Required]
        public string Bucket { get; set; }

        public string Path { get; set; }

        [Required]
        public string Object { get; set; }
        public bool MD5 { get; set; }
    }
}
