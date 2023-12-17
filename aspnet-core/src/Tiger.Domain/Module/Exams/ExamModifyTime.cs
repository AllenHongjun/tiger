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
        /// <summary>
        /// 租户Id
        /// </summary>
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
        /// 组织Id        
        /// </summary>
        public Guid OrganizationUnitId { get; set; }

        /// <summary>
        /// 考生Id
        /// </summary>
        public Guid ExamineeId { get; set; }

        //public virtual Exam Exam { get; set; }
        

    protected ExamModifyTime()
    {
    }

    public ExamModifyTime(
        Guid id,
        Guid? tenantId,
        Guid examId,
        int delayTime,
        int extendTime,
        Guid organizationUnitId,
        Guid examineeId
    ) : base(id)
    {
        TenantId = tenantId;
        ExamId = examId;
        DelayTime = delayTime;
        ExtendTime = extendTime;
        OrganizationUnitId = organizationUnitId;
        ExamineeId = examineeId;
    }
    }
}
