using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 题目 多评委评分表
    /// </summary>
    public class AnswerSheetDetailScore
    {
        /// <summary>
        /// 评卷人 ID
        /// </summary>
        public int JudgeUserId { get; set; }

        /// <summary>
        /// 评卷人用户名
        /// </summary>
        public string JudgeUserName { get; set; }

        /// <summary>
        /// 答卷ID
        /// </summary>
        public int AnswerSheetId { get; set; }

        /// <summary>
        /// 答卷明细表ID
        /// </summary>
        public int AnswerSheetDetailId { get; set; }

        /// <summary>
        /// 实操题人工评分
        /// </summary>
        public decimal? OperateManualScore { get; set; }

        /// <summary>
        /// 实操题人工评分时间
        /// </summary>
        public DateTime? OperateManualScoreTime { get; set; }

        /// <summary>
        /// 试卷ID
        /// </summary>
        public int TestPaperId { get; set; }
        /// <summary>
        /// 试题ID
        /// </summary>
        public int QuestionId { get; set; }

    }
}
