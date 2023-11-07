using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.Schools;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 考生
    /// </summary>
    public class Examinee : Entity<long>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid StudentId { get; set; }

        public Guid ClassId { get; set; }

        public Guid SchoolId { get; set; }

        //public virtual IdentityUser Student { get; set; }

        //public virtual ClassInfo ClassInfo { get; set; }

        //public virtual School School { get; set; }
    }
}
