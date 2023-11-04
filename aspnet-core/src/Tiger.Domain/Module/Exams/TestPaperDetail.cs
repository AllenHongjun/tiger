﻿using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.TestQuestions;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 试卷内容(题目)表
    /// </summary>
    public class TestPaperDetail:AuditedEntity<Guid>
    {
        /// <summary>
        /// 试卷ID
        /// </summary>
        public Guid TestPaperID { get; set; }
        /// <summary>
        /// 试题ID
        /// </summary>
        public Guid QuestionID { get; set; }

        /// <summary>
        /// 选题方式 1.自主选题 2.随机生成
        /// </summary>
        public TestPaperType TestPaperType { get; set; }

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
        public Guid QuestionCategoryId { get; set; }

        /// <summary>
        /// 难易度：1.简单 2.普通 3.困难
        /// </summary>
        public QuestionDegree QuestionDegree { get; set; }

        /// <summary>
        /// 题库
        /// </summary>
        public QuestionCategory QuestionCategory { get; set; }

        public TestPaper TestPaper { get; set; }

        public Question Question { get; set; }
    }
}