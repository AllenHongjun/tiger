using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.Schools;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 考试人员表(应试人；参加考试者)
    /// </summary>
    public class Examinee : CreationAuditedAggregateRoot<Guid>, IMultiTenant
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
        /// 考生Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 考生用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 考生所属组织Id
        /// </summary>
        public Guid OrganizationUnitId { get; set; }

        /// <summary>
        /// 考生所属组织名称
        /// </summary>
        public string OrganizationUnitName { get; set; }
        
    }
}
