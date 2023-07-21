﻿using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.System.Platform.Versions.Dto
{
    public class VersionDto : EntityDto<Guid>
    {
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 重要级别
        /// </summary>
        public ImportantLevel Level { get; set; }
        /// <summary>
        /// 文件列表
        /// </summary>
        public List<VersionFileDto> Files { get; set; } = new List<VersionFileDto>();
    }
}
