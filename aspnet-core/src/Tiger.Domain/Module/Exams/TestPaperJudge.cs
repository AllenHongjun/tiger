using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 试卷评委表
    /// </summary>
    /// <remarks>
    /// 评卷人只有关联了试卷才能改卷（默认只有超管能改卷）
    /// </remarks>
    public class TestPaperJudge: CreationAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        public Guid? TenantId { get; set; }

        /// 试卷Id
        /// </summary>
        public int TestPaperId { get; set; }

        /// <summary>
        /// 评卷人Id
        /// </summary>
        public Guid JudgeId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 组织Id
        /// </summary>
        public Guid OrganizationUnitId { get; set; }
        

        protected TestPaperJudge()
        {
        }

        public TestPaperJudge(
            Guid id,
            Guid? tenantId,
            int testPaperId,
            Guid judgeId,
            string userName,
            string fullName,
            string email,
            string phoneNumber,
            Guid organizationUnitId
        ) : base(id)
        {
            TenantId = tenantId;
            TestPaperId = testPaperId;
            JudgeId = judgeId;
            UserName = userName;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            OrganizationUnitId = organizationUnitId;
        }
    }
}
