using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.AuditLogging.Dto
{   
    /// <summary>
    /// 实体属性变更
    /// </summary>
    public class EntityPropertyChangeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 新值
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// 原始值
        /// </summary>
        public string OriginalValue { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 属性类型
        /// </summary>
        public string PropertyTypeFullName { get; set; }
    }
}
