using Microsoft.AspNetCore.Identity;
using System;
using Tiger.Module.Schools;
using Volo.Abp.Domain.Entities;

namespace Tiger.Volo.Abp.Identity.Users
{
    /// <summary>
    /// 班级和教师师关联表
    /// </summary>
    public class UserClass : Entity<Guid>
    {
        /// <summary>
        /// 班级Id
        /// </summary>
        public Guid ClassId { get; set; }

        /// <summary>
        /// 教师Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId { get; set; }


        public ClassInfo Class { get; set; }


        public IdentityUser User { get; set; }
    }
}
