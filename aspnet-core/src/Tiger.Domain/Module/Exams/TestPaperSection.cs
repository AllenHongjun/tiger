﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 试卷大题
    /// </summary>
    public class TestPaperSection:FullAuditedAggregateRoot<Guid>
    {
        public Guid TestPaperId { get;set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 大题描述(可用于保存阅读理解题、论述题、组合题等各种复杂题型的题干内容)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 题目数量
        /// </summary>
        public int QuestionCount { get; set; }

        /// <summary>
        /// 大题题目总分
        /// </summary>
        public decimal TotalScore { get; set; }

        public virtual TestPaper TestPaper { get; set; }

        public virtual ICollection<TestPaperQuestion> Questions { get; set; }
    }
}