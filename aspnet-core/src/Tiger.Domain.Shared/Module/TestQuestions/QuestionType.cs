using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.TestQuestions
{
    /// <summary>
    /// 题目类型
    /// </summary>
    public enum QuestionType
    {
        /// <summary>
        /// 判断题
        /// </summary>
        TrueOrFalse = 1,

        /// <summary>
        /// 单选题
        /// </summary>
        SingleChoice = 2,

        /// <summary>
        /// 多选题
        /// </summary>
        MultipleChoice = 3,

        /// <summary>
        /// 填空题
        /// </summary>
        Completion = 4,

        /// <summary>
        /// 计算题
        /// </summary>
        CalculationQuestions = 5,

        /// <summary>
        /// 问答题
        /// </summary>
        QA = 6,

        /// <summary>
        /// B型题
        /// </summary>
        Btype = 7,

        /// <summary>
        /// 简答题
        /// </summary>
        ShortAnswer = 8,

        /// <summary>
        /// 实训题
        /// </summary>
        PracticalTraining = 9,
    }
}
