using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 评卷分析
    /// </summary>
    public class JudgePaperAnalysisInfo
    {
        public Guid ExamId { get; set; }

        public int SubmitedCount { get; set; }

        public int ReviewedCount { get; set; }
    }
}
