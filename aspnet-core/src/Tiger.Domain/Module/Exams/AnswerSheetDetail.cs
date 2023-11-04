using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.TestQuestions;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 答卷明细表
    /// </summary>
    public class AnswerSheetDetail:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 答卷ID
        /// </summary>
        public Guid AnswerSheetId { get; set; }
        /// <summary>
        /// 试卷ID
        /// </summary>
        public Guid TestPaperId { get; set; }
        /// <summary>
        /// 试题ID
        /// </summary>
        public Guid QuestionId { get; set; }
        /// <summary>
        /// 答案
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// 试卷详情ID
        /// </summary>
        public int TestPaperDetailId { get; set; }

        /// <summary>
        /// 实操题自动评分分数
        /// </summary>
        public double OperateAutoScore { get; set; }
        /// <summary>
        /// 实操题人工评分分数
        /// </summary>
        public double OperateManualScore { get; set; }
        /// <summary>
        /// 实操ID
        /// </summary>
        public string OperateID { get; set; }
        
        /// <summary>
        /// 上次同步时间
        /// </summary>
        public DateTime? SyncTime { get; set; }

        /// <summary>
        /// 上次同步结果
        /// </summary>
        public string SyncMessage { get; set; }

        public AnswerSheet AnswerSheet { get; set; }

        public TestPaper TestPaper { get; set; }

        public Question Question { get; set; }
    }
}
