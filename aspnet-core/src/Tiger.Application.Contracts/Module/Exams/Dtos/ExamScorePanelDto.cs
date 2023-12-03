using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.Exams.Dtos
{
    /// <summary>
    /// 考试成绩面板数据
    /// </summary>
    public class ExamScorePanelDto
    {
        /// <summary>
        /// 参加人数
        /// </summary>
        public int NumberOfParticipants { get;set; }

        /// <summary>
        /// 及格次数
        /// </summary>
        public int NumberOfPasses { get; set; }

        /// <summary>
        /// 不及格次数
        /// </summary>
        public int NumberOfFailedPasses { get; set; }

        /// <summary>
        /// 得分率
        /// </summary>
        public decimal? ScoringRate { get; set; }

        /// <summary>
        /// 最高分
        /// </summary>
        public decimal? HighestScore { get; set; }

        /// <summary>
        /// 平均分
        /// </summary>
        public decimal? AverageScore { get; set; }

        /// <summary>
        /// 最低分
        /// </summary>
        public decimal? LowestScore { get; set; }

    }
}
