﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Schools
{
    /// <summary>
    /// 班级
    /// </summary>
    public class Class:FullAuditedEntity<Guid>, IMultiTenant
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


        public School School { get; set;}

        /// <summary>
        /// 班级的老师和学生
        /// </summary>
        public ICollection<IdentityUser> Users { get; set; }

        
    }
}
