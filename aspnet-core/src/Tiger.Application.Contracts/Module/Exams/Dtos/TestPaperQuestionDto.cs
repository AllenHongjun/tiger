using System;
using System.Collections.Generic;
using Tiger.Module.QuestionBank;
using Tiger.Module.QuestionBank.Dtos;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Exams.Dtos;

/// <summary>
/// 试卷内容(题目)表
/// </summary>
[Serializable]
public class TestPaperQuestionDto : AuditedEntityDto<Guid>
{
    /// <summary>
    /// 试卷ID
    /// </summary>
    public Guid TestPaperId { get; set; }

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
    /// 题目
    /// </summary>
    public QuestionDto question { get; set; }
}