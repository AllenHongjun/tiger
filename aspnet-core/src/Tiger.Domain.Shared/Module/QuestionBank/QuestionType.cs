using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.QuestionBank
{
    /// <summary>
    /// 题目类型
    /// </summary>
    public enum QuestionType
    {
        None = 0,
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
        [Obsolete]
        Calculation = 5,

        /// <summary>
        /// 问答题
        /// </summary>
        QA = 6,

        /// <summary>
        /// B型题
        /// </summary>
        [Obsolete]
        Btype = 7,

        /// <summary>
        /// 简答题
        /// </summary>
        [Obsolete]
        ShortAnswer = 8,

        /// <summary>
        /// 实训题
        /// </summary>
        PracticalTraining = 9,
    }

    
}
