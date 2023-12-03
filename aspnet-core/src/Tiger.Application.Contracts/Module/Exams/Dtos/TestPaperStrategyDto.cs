using System;
using Tiger.Module.QuestionBank;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Exams.Dtos;

/// <summary>
/// 组卷策略配置表
/// </summary>
[Serializable]
public class TestPaperStrategyDto : FullAuditedEntityDto<Guid>
{
    /// <summary>
    /// 试卷
    /// </summary>
    public Guid TestPaperId { get; set; }

    /// <summary>
    /// 试卷大题Id
    /// </summary>
    public Guid TestPaperSectionId { get; set; }

    /// <summary>
    /// 题目分类Id
    /// </summary>
    public Guid QuestionCategoryId { get; set; }

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
    public int SimpleCount { get; set; }

    /// <summary>
    /// 普通的数量
    /// </summary>
    public int OrdinaryCount { get; set; }

    /// <summary>
    /// 困难的数量
    /// </summary>
    public int DifficultCount { get; set; }

    /// <summary>
    /// 总抽题数量
    /// </summary>
    public int TotalSelectQuestionsCount { get; set; }

    /// <summary>
    /// 当前题库分类不限难度题目的总数量
    /// </summary>
    public int TotalUnlimitedDifficultyCount { get; set; }

    /// <summary>
    /// 当前题库分类简单题的总数量
    /// </summary>
    public int TotalSimpleCount { get; set; }

    /// <summary>
    /// 当前题库分类普通题的总数量
    /// </summary>
    public int TotalOrdinaryCount { get; set; }

    /// <summary>
    /// 当前题库分类困难题的总数量
    /// </summary>
    public int TotalDifficultCount { get; set; }

    /// <summary>
    /// 当前题库分类题目的总数量
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// 每题分数
    /// </summary>
    public decimal ScorePerQuestion { get; set; }

    /// <summary>
    /// 试卷大题
    /// </summary>
    public TestPaperSectionDto TestPaperSection { get; set; }
}