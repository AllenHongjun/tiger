using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.System.Platform.Datas.Dtos
{   
    /// <summary>
    /// 字典项目
    /// </summary>
    public class DataItemDto:EntityDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 允许为空
        /// </summary>
        public bool AllowBeNull { get; set; }

        /// <summary>
        /// 数据类型
        /// 
        /// </summary>
        public ValueType ValueType { get; set; }
    }
}
