using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Validation;

namespace Tiger.Module.OssManagement.Dtos
{
    public class UploadFileInput
    {
        public string Path { get; set; }
        public string Object { get; set; }
        public bool Overwrite { get; set; } = true;

        [Required]
        [DisableAuditing]
        [DisableValidation]
        public Stream File { get; set; }
    }
}
