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
        public QuestionType Type { get; set; }

        public int QuestionCount { get; set; }

        public int UnlimitedDifficultyCount { get; set; }

        public int SimpleCount { get; set; }

        public int OrdinaryCount { get; set; }

        public int DifficultCount { get; set; }
    }
}
