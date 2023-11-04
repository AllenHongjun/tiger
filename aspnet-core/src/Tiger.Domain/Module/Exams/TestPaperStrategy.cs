using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.TestQuestions;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 组卷策略 配置表
    /// </summary>
    public class TestPaperStrategy
    {
        /// <summary>
        /// 试卷
        /// </summary>
        public int TestPaperId { get; set; }

        /// <summary>
        /// 题目分类Id
        /// </summary>
        public Guid QuestionCategoryId { get; set; }    
        
        /// <summary>
        /// 题型
        /// </summary>
        public QuestionType QuestionType { get; set; }
        
        /// <summary>
        /// 简单的数量
        /// </summary>
        public int EasyCount { get; set; }
        
        /// <summary>
        /// 普通的数量
        /// </summary>
        public int OrdinaryCount { get; set; }
        
        /// <summary>
        /// 困难的数量
        /// </summary>
        public int DifficultCount { get; set; }

        /// <summary>
        /// 题目分类
        /// </summary>
        public QuestionCategory QuestionCategory { get; set; }
    }
}
