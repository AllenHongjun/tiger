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
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName { get;set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }
        
        

        protected Examinee()
        {
        }

        public Examinee(
            Guid id,
            Guid? tenantId,
            Guid examId,
            Guid userId,
            string userName,
            string fullName,
            string email,
            string phoneNumber
        ) : base(id)
        {
            TenantId = tenantId;
            ExamId = examId;
            UserId = userId;
            UserName = userName;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
