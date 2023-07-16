using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Module.OssManagement.Dtos
{
    public class FileShareInput
    {
        [Required]
        public string Name { get; set; }

        public string Path { get; set; }

        public int MaxAccessCount { get; set; }

        public DateTime? ExpirationTime { get; set; }

        public string[] Roles { get; set; }

        public string[] Users { get; set; }
    }
}
