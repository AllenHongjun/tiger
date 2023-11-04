using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.Teaching;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Module.Exams
{
    public class TestPaper:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型 1.固定题目（手动或自动选题） 2.随机题目（根据策略每个学员的题目都不同） 3.固定题目打乱显示
        /// </summary>
        public TestPaperType Type { get; set; }
        /// <summary>
        /// 是否已组卷
        /// 固定题目需要组卷，随机选题不需要
        /// </summary>
        public bool IsCreate { get; set; }
        
        /// <summary>
        /// 状态
        /// </summary>
        public bool IsEnable { get; set; }



        // 限制哪些用户可以改这些试卷

        /// <summary>
        /// 学校ID
        /// </summary>
        public int SchoolID { get; set; }

        /// <summary>
        /// 是否包含全校老师
        /// </summary>
        public bool IsSchool { get; set; }

        /// <summary>
        /// 是否限制评卷时间
        /// </summary>
        public bool LimitJudgeTime { get; set; }

        /// <summary>
        /// 老师
        /// </summary>
        public int UserID { get; set; }
        
        /// <summary>
        /// 评卷开始时间
        /// </summary>
        public DateTime JudgeStartTime { get; set; }

        /// <summary>
        /// 评卷结束时间
        /// </summary>
        public DateTime JudgeEndTime { get; set; }

        /// <summary>
        /// 主试卷、固定题目时0，随机题目或打乱顺序时录入主试卷的ID
        /// </summary>
        public Guid TestPaperMainId { get; set; }

        public Guid CourseId { get;set; }

        public Course Course { get; set; }
    }
}
