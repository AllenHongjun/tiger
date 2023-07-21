﻿using Newtonsoft.Json;

namespace Tiger.Module.System.Platform.Versions.Dto
{
    public class VersionFileDto
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 文件SHA256编码
        /// </summary>
        public string SHA256 { get; set; }
        /// <summary>
        /// 文件大小
        /// 单位b
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType FileType { get; set; }
    }
}
