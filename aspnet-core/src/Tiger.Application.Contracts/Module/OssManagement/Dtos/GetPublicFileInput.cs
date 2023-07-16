using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Module.OssManagement.Dtos
{
    public class GetPublicFileInput
    {
        [Required]
        public string Name { get; set; }

        public string Path { get; set; }

        public string Process { get; set; }
    }
}
