using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Module.OssManagement.Dtos
{
    public class UploadFile
    {
        /// <summary>
        /// 总文件大小
        /// </summary>
        [Required]
        public long TotalSize { get;set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [Required]
        public string FileName { get; set; }

    }
}
