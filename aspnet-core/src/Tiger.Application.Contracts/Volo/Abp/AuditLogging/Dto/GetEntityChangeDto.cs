using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Data;

namespace Tiger.Volo.Abp.AuditLogging.Dto
{
    public class GetEntityChangeDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 审计日志id
        /// </summary>
        public Guid? AuditLogId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 实体变更类型 0:创建； 1:更新；  2:删除
        /// </summary>
        public EntityChangeType? ChangeType { get; set; }

        /// <summary>
        /// 实体Id
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// 实体类型全称
        /// </summary>
        public string EntityTypeFullName { get; set; }

        /// <summary>
        /// 是否包含明细
        /// </summary>
        public bool? IncludeDetails { get; set; }

    }
}
