using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Module.Exams
{
    public class AnswerSheetScore:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 答卷表ID
        /// </summary>
        public Guid AnswerSheetId { get; set; }

        public Guid StudentId { get; set; }

        /// <summary>
        /// 考试表ID
        /// </summary>
        public Guid ExamId { get; set; }

        /// <summary>
        /// 评卷人Id
        /// </summary>
        public Guid JudgeUserId { get; set; }

        /// <summary>
        /// 评卷人用户名
        /// </summary>
        public string JudgeUserName { get; set; }

        /// <summary>
        /// 实操人工阅卷分数
        /// </summary>
        public decimal? OperateManualScore { get; set; }
        /// <summary>
        /// 实操人工评分时间
        /// </summary>
        public DateTime? OperateManualScoreTime { get; set; }

    }
}
