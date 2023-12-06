using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.Exams
{
    public class OrganizationUnitAnalysisInfo
    {
        /// <summary>
        /// 组织名称
        /// </summary>
        public string OrganizationUnitName { get; set; }

        /// <summary>
        /// 应参加人数
        /// </summary>
        public int NumberOfParticipants { get; set; }

        /// <summary>
        /// 实际参加人数
        /// </summary>
        public int ActualNumberOfParticipants { get; set; }

        /// <summary>
        /// 参考率
        /// </summary>
        public decimal ParticipantRate { get; set; }

        /// <summary>
        /// 及格人数
        /// </summary>
        public int NumberPassed { get; set; }

        /// <summary>
        /// 及格率
        /// </summary>
        public decimal PassingRate { get; set; }

        /// <summary>
        /// 正确率
        /// </summary>
        public decimal CorrectRate {  get; set; }

        /// <summary>
        /// 得分率
        /// </summary>
        public decimal? ScoringRate { get; set; }

        /// <summary>
        /// 最高分
        /// </summary>
        public decimal? HighestScore { get; set; }

        /// <summary>
        /// 最低分
        /// </summary>
        public decimal? LowestScore {  get; set; }

        /// <summary>
        /// 平均分
        /// </summary>
        public decimal? AverageScore { get; set; }

    }
}
