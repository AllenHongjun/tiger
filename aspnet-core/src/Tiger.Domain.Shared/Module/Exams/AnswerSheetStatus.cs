using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 答卷状态
    /// </summary>
    public enum AnswerSheetStatus
    {
        /// <summary>
        /// 未阅卷(考试中)
        /// </summary>
        UnSubmit = 1,

        /// <summary>
        /// 已交卷
        /// </summary>
        Submited = 2,

        /// <summary>
        /// 已评卷
        /// </summary>
        Reviewed = 3,
    }
}
