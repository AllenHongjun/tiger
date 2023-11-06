using System;
using System.ComponentModel;
using Tiger.Module.QuestionBank;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Exams.Dtos;

[Serializable]
public class TestPaperStrategyGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 试卷
    /// </summary>
    public Guid? TestPaperId { get; set; }

    /// <summary>
    /// 题目分类Id
    /// </summary>
    public Guid? QuestionCategoryId { get; set; }

    /// <summary>
    /// 题型
    /// </summary>
    public QuestionType? QuestionType { get; set; }

    /// <summary>
    /// 不限难度数量
    /// </summary>
    public int? UnlimitedDifficultyCount { get; set; }

    /// <summary>
    /// 简单的数量
    /// </summary>
    public int? EasyCount { get; set; }

    /// <summary>
    /// 普通的数量
    /// </summary>
    public int? OrdinaryCount { get; set; }

    /// <summary>
    /// 困难的数量
    /// </summary>
    public int? DifficultCount { get; set; }
}