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
        public ExamType ExamType { get; set; }
        
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
        public decimal? DeductionAmounnt { get; set; }

        /// <summary>
        /// 扣款间隔（单位: 分钟）
        /// </summary>
        public int? DeductionInterval { get; set; }

        /// <summary>
        /// 比赛间隔（单位: 分钟）
        /// </summary>
        public int? Interval { get; set; }

        /// <summary>
        /// 课程
        /// </summary>
        //public virtual Course Course { get; set; }

        /// <summary>
        /// 试卷
        /// </summary>
        public virtual TestPaper TestPaper { get; set; }

        /// <summary>
        /// 答卷
        /// </summary>
        public virtual ICollection<AnswerSheet> AnswerSheets { get; set; }

        /// <summary>
        /// 题目分类
        /// </summary>
        //public virtual QuestionCategory QuestionCategory { get; set; }

        protected Exam()
    {
    }

    public Exam(
        Guid id,
        Guid? courseId,
        Guid testPaperId,
        Guid questionCategoryId,
        string name,
        string coverUrl,
        string number,
        ExamType examType,
        DateTime startDate,
        DateTime endDate,
        int examDuration,
        bool isDifferent,
        bool isDifferentOrder,
        bool isShowScore,
        bool isShowError,
        bool isEnable,
        bool isExamAnyTime,
        bool isShowWindowOnblur,
        int maxExamCount,
        int sorting,
        bool onlyExamDayVisible,
        bool isStartSync,
        bool isShowHelp,
        bool halftimeFlag,
        DateTime halftimeStart,
        DateTime halftimeEnd,
        decimal? deductionAmounnt,
        int? deductionInterval,
        int? interval
    ) : base(id)
    {
        CourseId = courseId;
        TestPaperId = testPaperId;
        QuestionCategoryId = questionCategoryId;
        Name = name;
        CoverUrl = coverUrl;
        Number = number;
        ExamType = examType;
        StartDate = startDate;
        EndDate = endDate;
        ExamDuration = examDuration;
        IsDifferent = isDifferent;
        IsDifferentOrder = isDifferentOrder;
        IsShowScore = isShowScore;
        IsShowError = isShowError;
        IsEnable = isEnable;
        IsExamAnyTime = isExamAnyTime;
        IsShowWindowOnblur = isShowWindowOnblur;
        MaxExamCount = maxExamCount;
        Sorting = sorting;
        OnlyExamDayVisible = onlyExamDayVisible;
        IsStartSync = isStartSync;
        IsShowHelp = isShowHelp;
        HalftimeFlag = halftimeFlag;
        HalftimeStart = halftimeStart;
        HalftimeEnd = halftimeEnd;
        DeductionAmounnt = deductionAmounnt;
        DeductionInterval = deductionInterval;
        Interval = interval;
    }
    }
}
