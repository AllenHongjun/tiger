using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.Teachings;
using Tiger.Module.QuestionBank;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 考试
    /// </summary>
    public class Exam:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 课程Id
        /// </summary>
        public Guid? CourseId { get; set; }

        /// <summary>
        /// 考试的试卷（母卷）
        /// </summary>
        public Guid TestPaperId { get; set; }

        /// <summary>
        /// 题目分类
        /// </summary>
        public Guid QuestionCategoryId { get; set; }

        /// <summary>
        /// 考试名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 封面
        /// </summary>
        public string CoverUrl { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 类型：1.考试 2.练习 , 3 比赛
        /// </summary>
        public bool IsEaxm { get; set; }

        /// <summary>
        /// 题型：1.实操考试 2.理论考试 
        /// </summary>
        public int ExamType { get; set; }
        
        /// <summary>
        /// 考试时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 考试时长 单位：分钟
        /// </summary>
        public int ExamDuration { get; set; }

        /// <summary>
        /// 是否每个人都不同
        /// </summary>
        public bool IsDifferent { get; set; }

        /// <summary>
        /// 顺序不同（试卷相同时）
        /// </summary>
        public bool IsDifferentOrder { get; set; }

        /// <summary>
        /// 提交后是否显示成绩
        /// </summary>
        public bool IsShowScore { get; set; }

        /// <summary>
        /// 是否可以查看错题
        /// </summary>
        public bool IsShowError { get; set; }
        
        /// <summary>
        /// 启用状态
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 是否随到随考
        /// </summary>
        public bool IsExamAnyTime { get; set; }

        /// <summary>
        /// 提示切屏次数
        /// </summary>
        public bool IsShowWindowOnblur { get; set; }
        
        /// <summary>
        /// 考试最大次数
        /// </summary>
        public int MaxExamCount { get; set; }
        
        /// <summary>
        /// 顺序
        /// </summary>
        public int Sorting { get; set; }

        /// <summary>
        /// 仅考试当天可见
        /// </summary>
        public bool OnlyExamDayVisible { get; set; }

        /// <summary>
        /// 是否启动自动实操评分
        /// </summary>
        public bool IsStartSync { get; set; }

        /// <summary>
        /// 是否显示帮助内容
        /// </summary>
        public bool IsShowHelp { get; set; }

        /// <summary>
        /// 是否中场休息
        /// </summary>
        public bool HalftimeFlag { get; set; }

        /// <summary>
        /// 中场休息开始时间
        /// </summary>
        public DateTime HalftimeStart { get; set; }

        /// <summary>
        /// 中场休息结束时间
        /// </summary>
        public DateTime HalftimeEnd { get; set; }

        /// <summary>
        /// 扣款金额
        /// </summary>
        public decimal DeductionAmounnt { get; set; }

        /// <summary>
        /// 扣款间隔（单位: 分钟）
        /// </summary>
        public int DeductionInterval { get; set; }

        /// <summary>
        /// 比赛间隔（单位: 分钟）
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// 考试学校 格式：,1,2,
        /// 不选学校就是公开
        /// </summary>
        public string SchoolID { get; set; }
        /// <summary>
        /// 是否是全校的考试
        /// </summary>
        public bool IsSchool { get; set; }
        /// <summary>
        /// 是否是全班的考试
        /// </summary>
        public bool IsClass { get; set; }
        /// <summary>
        /// 考试班级 格式：,1,2,
        /// 不选班级就是公开
        /// </summary>
        public string ClassID { get; set; }
        /// <summary>
        /// 学生数量
        /// </summary>
        public int StudentQuantity { get; set; }

        /// <summary>
        /// 当是个别学员的考试，则存入学员ID
        /// </summary>
        public string Students { get; set; }

        


        public Course Course { get; set; }

        public TestPaper TestPaper { get; set; }

        /// <summary>
        /// 题目分类
        /// </summary>
        public QuestionCategory QuestionCategory { get; set; }
    }
}
