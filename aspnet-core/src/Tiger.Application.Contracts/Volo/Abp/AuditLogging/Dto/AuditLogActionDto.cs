using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;

namespace Tiger.Volo.Abp.AuditLogging.Dto
{
    public class AuditLogActionDto : EntityDto<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 服务名称
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

        /// <summary>
        /// 执行持续时间
        /// </summary>

        public int ExecutionDuration { get; set; }

        /// <summary>
        /// 附加参数
        /// </summary>
        public Dictionary<string, object> ExtraProperties { get; set; }

    }
}
