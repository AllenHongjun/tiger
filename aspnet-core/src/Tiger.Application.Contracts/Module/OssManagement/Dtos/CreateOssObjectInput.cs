using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Validation;

namespace Tiger.Module.OssManagement.Dtos
{
    public class CreateOssObjectInput
    {
        public string Bucket { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public bool Overwrite { get; set; }

        [DisableAuditing]
        [DisableValidation]
        public Stream File { get; set; }

        public TimeSpan? ExpirationTime { get; set; }

        public void SetContent(Stream content)
        {
            content.Seek(0, SeekOrigin.Begin);
            File = content;
        }
    }
}
