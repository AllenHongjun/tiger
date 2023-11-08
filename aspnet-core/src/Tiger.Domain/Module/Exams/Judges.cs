using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.Schools;
using Volo.Abp.Identity;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 试卷评委
    /// </summary>
    public class Judges
    {
        /// <summary>
        /// 试卷Id
        /// </summary>
        public Guid TestPaperId { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>
        public Guid SchoolId { get; set; }

        /// <summary>
        /// 班级Id
        /// </summary>
        public Guid ClassId { get; set; }

        /// <summary>
        /// 评委Id
        /// </summary>
        public Guid JudgeUserId { get; set; }

        public virtual TestPaper TestPaper { get; set; }

        public virtual School School { get; set; }

        public virtual ClassInfo ClassInfo { get; set; }

        public virtual IdentityUser JudgeUser { get; set; }
    }
}
