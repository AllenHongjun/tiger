using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 试卷大题类型
    /// </summary>
    public enum TestPaperSectionType
    {
        /// <summary>
        /// 固定题目大题
        /// </summary>
        FixedQuestions = 1,

        /// <summary>
        /// 随机题目大题
        /// </summary>
        RandomQuestion = 2,

        /// <summary>
        /// 抽题大题
        /// </summary>
        ExtractQuestions = 3,
    }
}
