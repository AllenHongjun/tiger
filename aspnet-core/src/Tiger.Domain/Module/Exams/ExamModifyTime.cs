using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 考试时间调整表
    /// </summary>
    public class ExamModifyTime:FullAuditedEntity<Guid>,IMultiTenant
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 考试Id
        /// </summary>
        public Guid ExamId { get; set; }

        /// <summary>
        /// 考试推迟时间：单位分钟
        /// </summary>
        public int DelayTime { get; set; }

        /// <summary>
        /// 考试延长时间：单位分钟
        /// </summary>
        public int ExtendTime { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>
        public Guid SchoolId { get; set; }

        /// <summary>
        /// 班级Id
        /// </summary>
        public Guid ClassId { get; set; }

        /// <summary>
        /// 学员Id
        /// </summary>
        public Guid StudentId { get; set; }

        public virtual Exam Exam { get; set; }
        
    }
}
