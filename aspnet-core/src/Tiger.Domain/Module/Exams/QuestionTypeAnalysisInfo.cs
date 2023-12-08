using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.QuestionBank;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 试题类型分析
    /// </summary>
    public class QuestionTypeAnalysisInfo
    {
        /// <summary>
        /// 题型
        /// </summary>
        public QuestionType Type { get; set; }

        /// <summary>
        /// 试题总数
        /// </summary>
        public int QuestionCount { get; set; }

        /// <summary>
        /// 不限难度数量
        /// </summary>
        public int UnlimitedDifficultyCount { get; set; }

        /// <summary>
        /// 容易题数量
        /// </summary>
        public int SimpleCount { get; set; }

        /// <summary>
        /// 普通难度数量
        /// </summary>
        public int OrdinaryCount { get; set; }

        /// <summary>
        /// 困难题数量
        /// </summary>
        public int DifficultCount { get; set; }
    }
}
