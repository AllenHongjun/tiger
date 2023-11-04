using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Schools
{
    /// <summary>
    /// 班级
    /// </summary>
    /// <remarks>
    /// 为了和 class关键子区分 添加info后缀
    /// </remarks>
    public class ClassInfo:FullAuditedEntity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 学校
        /// </summary>
        public Guid SchoolId { get; set; }
        
        /// <summary>
        /// 顺序
        /// </summary>
        public int Sorting { get; set; }
        
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 学校
        /// </summary>
        public virtual School School { get; set;}

        /// <summary>
        /// 班级的老师和学生
        /// </summary>
        public virtual ICollection<IdentityUser> Users { get; set; }

        

        protected ClassInfo()
        {
        }

        public ClassInfo(
            Guid id,
            Guid? tenantId,
            string name,
            Guid schoolId,
            int sorting,
            bool isEnable,
            School school,
            ICollection<IdentityUser> users
        ) : base(id)
        {
            TenantId = tenantId;
            Name = name;
            SchoolId = schoolId;
            Sorting = sorting;
            IsEnable = isEnable;
            School = school;
            Users = users;
        }
    }
}
