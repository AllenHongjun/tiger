using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;

namespace Volo.Abp.AuditLogging
{
    public class AuditLogActionDto : EntityDto<Guid>, IHasExtraProperties
    {   
        /// <summary>
        /// 
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecutionTime { get; set; }
        public int ExecutionDuration { get; set; }

        /// <summary>
        /// 附加参数
        /// </summary>
        public Dictionary<string, object> ExtraProperties { get; set; }

    }
}
