using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// 文件
        /// </summary>
        /// <remarks>
        /// RemoteStreamContent 无法直接在swaggerUi中生成 文件上传图标
        /// </remarks>
        [DisableAuditing]
        [DisableValidation]
        public IFormFile File { get; set; }


        public TimeSpan? ExpirationTime { get; set; }

        public void SetContent(Stream content)
        {
            content.Seek(0, SeekOrigin.Begin);
            //File = new RemoteStreamContent(content); ;
        }
    }
}
