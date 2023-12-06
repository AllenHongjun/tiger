using NUglify.JavaScript.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.QuestionBank;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 答卷明细表
    /// </summary>
    public class AnswerSheetDetail : FullAuditedEntity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        /// <summary>
        /// 答卷Id
        /// </summary>
        public Guid AnswerSheetId { get; set; }

        /// <summary>
        /// 试卷Id
        /// </summary>
        public Guid TestPaperId { get; set; }

        /// <summary>
        /// 试卷详情Id
        /// </summary>
        public Guid TestPaperDetailId { get; set; }

        /// <summary>
        /// 试题Id
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// 答案
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// 客观题评分
        /// </summary>
        public decimal? ObjectiveScore { get; set; }

        /// <summary>
        /// 实操题自动评分分数
        /// </summary>
        public decimal? OperateAutoScore { get; set; }

        /// <summary>
        /// 实操题人工评分分数
        /// </summary>
        public decimal? OperateManualScore { get; set; }

        /// <summary>
        /// 实操Id
        /// </summary>
        public string OperateId { get; set; }
        
        /// <summary>
        /// 上次同步时间
        /// </summary>
        public DateTime? SyncTime { get; set; }

        /// <summary>
        /// 上次同步结果
        /// </summary>
        public string SyncMessage { get; set; }

        /// <summary>
        /// 是否正确
        /// </summary>
        public bool? IsCorrect { get; set; }

        /// <summary>
        /// 题目总得分
        /// </summary>
        public decimal? Score { get; set; }

        /// <summary>
        /// 答卷
        /// </summary>
        public AnswerSheet AnswerSheet { get; set; }

        ///// <summary>
        ///// 试卷
        ///// </summary>
        //public TestPaper TestPaper { get; set; }

        ///// <summary>
        ///// 试题
        ///// </summary>
        //public Question Question { get; set; }

        protected AnswerSheetDetail()
        {
        }

        public AnswerSheetDetail(
            Guid id,
            Guid? tenantId,
            Guid answerSheetId,
            Guid testPaperId,
            Guid testPaperDetailId,
            Guid questionId,
            string answer,
            decimal? objectiveScore,
            decimal? operateAutoScore,
            decimal? operateManualScore,
            string operateId,
            DateTime? syncTime,
            string syncMessage
        ) : base(id)
        {
            TenantId = tenantId;
            AnswerSheetId = answerSheetId;
            TestPaperId = testPaperId;
            TestPaperDetailId = testPaperDetailId;
            QuestionId = questionId;
            Answer = answer;
            ObjectiveScore = objectiveScore;
            OperateAutoScore = operateAutoScore;
            OperateManualScore = operateManualScore;
            OperateId = operateId;
            SyncTime = syncTime;
            SyncMessage = syncMessage;
        }
    }
}
