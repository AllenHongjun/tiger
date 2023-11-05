using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Module.QuestionBank
{
    /// <summary>
    /// 题目答案
    /// </summary>
    public class QuestionAnswer:FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 题目Id
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// 题目答案
        /// </summary>
        /// <remarks>
        /// 如果一个填空有多个答案请用 & 开隔 ,三个连续下划线___ (提示：在英文输入法下，按Shift键+减号键可敲出下划线)
        /// </remarks>
        public string Answer { get; set; }  

        public virtual Question Question { get; set; }
    }
}
