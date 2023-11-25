using System;
using Tiger.Module.QuestionBank;

namespace Tiger.Module.Exams.Dtos;

[Serializable]
public class CreateUpdateTestPaperStrategyDto
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
    public decimal ScorePerQuestion { get; set; } = decimal.Zero;
}