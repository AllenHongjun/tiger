using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 答卷表
    /// </summary>
    public class AnswerSheet:FullAuditedEntity<Guid>,IMultiTenant
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 组织Id
        /// </summary>
        public Guid? OrganizationUnitId { get; set; }

        /// <summary>
        /// 主试卷、固定题目时0，随机题目或打乱顺序时录入主试卷的ID
        /// </summary>
        public int TestPaperMainId { get; set; }

        /// <summary>
        /// 试卷ID
        /// </summary>
        public Guid TestPaperId { get; set; }

        /// <summary>
        /// 考试ID
        /// </summary>
        public Guid ExamId { get; set; }

        /// <summary>
        /// 学员Id
        /// </summary>
        public Guid StudentId { get; set; }
        
        /// <summary>
        /// 是否交卷 True为交卷
        /// </summary>
        public bool IsSubmit { get; set; }

        /// <summary>
        /// 交卷时间
        /// </summary>
        public DateTime? SubmitDateTime { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 设备类型： 1.电脑 2.手机 3.平板
        /// </summary>
        public int DeviceType { get; set; }
        
        /// <summary>
        /// 考试总时长
        /// </summary>
        public int ExamDuration { get; set; }

        /// <summary>
        /// 答题总时长（分钟）
        /// </summary>
        public int AnswerTotalDuration { get; set; }

        /// <summary>
        /// 考试切屏次数
        /// </summary>
        public int WindowOnblur { get; set; }

        /// <summary>
        /// 阅卷时间
        /// </summary>
        public DateTime? ScoreTime { get; set; }

        /// <summary>
        /// 实操题自动评分
        /// </summary>
        public decimal? OperateAutoScore { get; set; }

        /// <summary>
        /// 实操自动评分时间
        /// </summary>
        public DateTime? OperateAutoScoreTime { get; set; }

        /// <summary>
        /// 实操题人工打分
        /// </summary>
        public decimal? OperateManualScore { get; set; }

        /// <summary>
        /// 实操题自动评分时间
        /// </summary>
        public DateTime? OperateManualScoreTime { get;set; }

        /// <summary>
        /// 客观题评分
        /// </summary>
        public decimal? ObjectiveScore { get; set; }
        /// <summary>
        /// 客观题评分时间
        /// </summary>
        public DateTime? ObjectiveScoreTime { get; set; }

        /// <summary>
        /// 总成绩
        /// </summary>
        public decimal? TotalScore { get; set; }

        /// <summary>
        /// 答卷状态:未交卷;2:已交卷;3:已阅卷
        /// </summary>
        public AnswerSheetStatus Status { get; set; }

        /// <summary>
        /// 是否及格
        /// </summary>
        public bool? IsPass { get;set; }

        public virtual Exam Exam { get; set; }

        public virtual ICollection<AnswerSheetDetail> AnswerSheetDetails { get; set; }

        //public virtual TestPaper TestPaper { get; set; }    

        //public virtual IdentityUser Student { get; set; }


        protected AnswerSheet()
    {
    }

    public AnswerSheet(
        Guid id,
        Guid? tenantId,
        int testPaperMainId,
        Guid testPaperId,
        Guid examId,
        Guid studentId,
        decimal totalScore,
        bool isSubmit,
        DateTime? submitDateTime,
        string iP,
        int deviceType,
        int examDuration,
        int answerTotalDuration,
        int windowOnblur,
        DateTime? scoreTime,
        decimal? operateAutoScore,
        DateTime? operateAutoScoreTime,
        decimal? operateManualScore,
        DateTime? operateManualScoreTime,
        decimal? objectiveScore,
        DateTime? objectiveScoreTime
    ) : base(id)
    {
        TenantId = tenantId;
        TestPaperMainId = testPaperMainId;
        TestPaperId = testPaperId;
        ExamId = examId;
        StudentId = studentId;
        TotalScore = totalScore;
        IsSubmit = isSubmit;
        SubmitDateTime = submitDateTime;
        IP = iP;
        DeviceType = deviceType;
        ExamDuration = examDuration;
        AnswerTotalDuration = answerTotalDuration;
        WindowOnblur = windowOnblur;
        ScoreTime = scoreTime;
        OperateAutoScore = operateAutoScore;
        OperateAutoScoreTime = operateAutoScoreTime;
        OperateManualScore = operateManualScore;
        OperateManualScoreTime = operateManualScoreTime;
        ObjectiveScore = objectiveScore;
        ObjectiveScoreTime = objectiveScoreTime;
    }
    }
}
