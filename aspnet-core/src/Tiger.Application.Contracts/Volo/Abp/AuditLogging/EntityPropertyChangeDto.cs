using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Volo.Abp.AuditLogging
{
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
        /// 属性原始值
        /// </summary>
        public string PropertyTypeFullName { get; set; }
    }
}
