using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Tiger.Module.Exams.Dtos;

[Serializable]
public class CreateUpdateTestPaperSectionDto
{
    public Guid TestPaperId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(TestPaperSectionConsts), nameof(TestPaperSectionConsts.MaxNameLength))]
    public string Name { get; set; }

    /// <summary>
    /// 大题描述(可用于保存阅读理解题、论述题、组合题等各种复杂题型的题干内容)
    /// </summary>
    [DynamicStringLength(typeof(TestPaperSectionConsts), nameof(TestPaperSectionConsts.MaxDesctiptionLength))]
    public string Description { get; set; }

    /// <summary>
    /// 题目数量
    /// </summary>
    public int QuestionCount { get; set; }

    /// <summary>
    /// 大题题目总分
    /// </summary>
    public decimal TotalScore { get; set; }

    /// <summary>
    /// 序号
    /// </summary>
    public int Sort { get; set; }
    
}