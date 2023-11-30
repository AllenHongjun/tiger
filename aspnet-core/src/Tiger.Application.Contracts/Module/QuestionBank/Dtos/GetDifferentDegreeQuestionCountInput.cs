using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.QuestionBank.Dtos
{
    public class GetDifferentDegreeQuestionCountInput
    {
        /// <summary>
        /// 题目分类Id
        /// </summary>
        public Guid QuestionCategoryId { get; set; }

        /// <summary>
        /// 类型 1.判断 2.单选 3.多选 4.填空 5.计算题 6.问答题 7.B型题,8.简答题 9.实训任务
        /// </summary>
        public QuestionType? Type { get; set; }
    }
}
