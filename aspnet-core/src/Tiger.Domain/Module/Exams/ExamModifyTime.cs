using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Module.Exams
{
    public class ExamModifyTime:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 考试ID
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
        /// 影响学校ID，用逗号隔开
        /// </summary>
        public string SchoolID { get; set; }
        /// <summary>
        /// 
        /// 影响班级ID，用逗号隔开
        /// </summary>
        public string ClassID { get; set; }
        /// <summary>
        /// 影响学生ID，用逗号隔开
        /// </summary>
        public string Students { get; set; }
        /// <summary>
        /// 是否学校里的部分班级受到影响
        /// </summary>
        public bool IsSchool { get; set; }
        /// <summary>
        /// 是否班级里的部分学生受到影响
        /// </summary>
        public bool IsClass { get; set; }
    }
}
