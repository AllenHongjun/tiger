using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.QuestionBank;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 试卷小题
    /// </summary>
    public class TestPaperQuestion:AuditedEntity<Guid>,IMultiTenant
    {
        public Guid? TenantId { get; set; }
        /// <summary>
        /// 试卷ID
        /// </summary>
        public Guid TestPaperId { get; set; }

        /// <summary>
        /// 试卷大题ID
        /// </summary>
        public Guid TestPaperSectionId { get; set; }

        /// <summary>
        /// 题目分类
        /// </summary>
        public Guid QuestionCategoryId { get; set; }

        /// <summary>
        /// 试题ID
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// 选题方式 1.自主选题 2.随机生成
        /// </summary>
        public TestPaperType TestPaperType { get; set; }

        /// <summary>
        /// 难易度：1.简单 2.普通 3.困难
        /// </summary>
        public QuestionDegree QuestionDegree { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int Sorting { get; set; }
        
        /// <summary>
        /// 每题分数
        /// </summary>
        public decimal Score { get; set; }

        /// <summary>
        /// 漏选按错误处理
        /// </summary>
        public bool MissOptionInvalid { get; set; }



        /// <summary>
        /// 题目分类
        /// </summary>
        //public virtual QuestionCategory QuestionCategory { get; set; }

        /// <summary>
        /// 试卷
        /// </summary>
        public virtual TestPaper TestPaper { get; set; }

        /// <summary>
        /// 试卷大题
        /// </summary>
        public virtual TestPaperSection TestPaperSection { get; set; }

        /// <summary>
        /// 题目
        /// </summary>
        //public virtual Question Question { get; set; }

        protected TestPaperQuestion()
        {
        }

        public TestPaperQuestion(
            Guid id,
            Guid? tenantId,
            Guid testPaperId,
            Guid questionCategoryId,
            Guid questionId,
            TestPaperType testPaperType,
            QuestionDegree questionDegree,
            int sorting,
            decimal score,
            bool missOptionInvalid
        ) : base(id)
        {
            TenantId = tenantId;
            TestPaperId = testPaperId;
            QuestionCategoryId = questionCategoryId;
            QuestionId = questionId;
            TestPaperType = testPaperType;
            QuestionDegree = questionDegree;
            Sorting = sorting;
            Score = score;
            MissOptionInvalid = missOptionInvalid;
        }
    }
}
