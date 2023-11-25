using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.QuestionBank;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 组卷策略配置表
    /// </summary>
    public class TestPaperStrategy:FullAuditedAggregateRoot<Guid>,IMultiTenant
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 试卷Id
        /// </summary>
        public Guid TestPaperId { get; set; }

        /// <summary>
        /// 试卷大题Id
        /// </summary>
        public Guid TestPaperSectionId { get; set; }

        /// <summary>
        /// 题目分类Id
        /// </summary>
        public Guid? QuestionCategoryId { get; set; }    
        
        /// <summary>
        /// 题型
        /// </summary>
        public QuestionType QuestionType { get; set; }
        
        /// <summary>
        /// 不限难度数量
        /// </summary>
        public int UnlimitedDifficultyCount { get; set; }

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
        /// 每题分数
        /// </summary>
        public decimal ScorePerQuestion { get; set; }

        /// <summary>
        /// 题目分类
        /// </summary>
        public virtual QuestionCategory QuestionCategory { get; set; }

        /// <summary>
        /// 试卷
        /// </summary>
        public virtual TestPaper TestPaper { get; set; }

        /// <summary>
        /// 试卷大题
        /// </summary>
        public virtual TestPaperSection TestPaperSection { get;set; }

        protected TestPaperStrategy()
        {
        }

        public TestPaperStrategy(
            Guid id,
            Guid? tenantId,
            Guid testPaperId,
            Guid? questionCategoryId,
            QuestionType questionType,
            int unlimitedDifficultyCount,
            int easyCount,
            int ordinaryCount,
            int difficultCount,
            decimal scorePerQuestion
        ) : base(id)
        {
            TenantId = tenantId;
            TestPaperId = testPaperId;
            QuestionCategoryId = questionCategoryId;
            QuestionType = questionType;
            UnlimitedDifficultyCount = unlimitedDifficultyCount;
            EasyCount = easyCount;
            OrdinaryCount = ordinaryCount;
            DifficultCount = difficultCount;
            ScorePerQuestion = scorePerQuestion;
        }
    }
}
