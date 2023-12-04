using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.QuestionBank;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 大题统计
    /// </summary>
    public class QuestionAnalysisInfo
    {

        /// <summary>
        /// 题目id
        /// </summary>
        public Guid? QuestionId { get; set; }
        /// <summary>
        /// 试题内容
        /// </summary>
        public string QuestionContent { get; set; }

        /// <summary>
        /// 题目类型
        /// </summary>
        public QuestionType QuestionType { get; set; }

        /// <summary>
        /// 试题分类ID
        /// </summary>
        public Guid QuestionCategoryId { get; set;}

        /// <summary>
        /// 试题分类名称
        /// </summary>
        public Guid QuestionCategoryName { get; set;}

        /// <summary>
        /// 试题难度
        /// </summary>
        public QuestionDegree Degree { get; set;}

        /// <summary>
        /// 试题答案
        /// </summary>
        public string Answer { get; set;}

        /// <summary>
        /// 答题次数
        /// </summary>
        public int NumberOfAnswers { get; set;}

        /// <summary>
        /// 打错次数
        /// </summary>
        public int NumberOfWrongAnswers { get; set; }

        /// <summary>
        /// 错误率
        /// </summary>
        public float ErrorRate { get; set; }

        /// <summary>
        /// 答对次数
        /// </summary>
        public int NumberOfRightAnswers { get; set; }

        /// <summary>
        /// 正确率
        /// </summary>
        public int CorrectRate { get; set; }

        /// <summary>
        /// 得分率
        /// </summary>
        public decimal? ScoringRate { get; set; }
    }
}
