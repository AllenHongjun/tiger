using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Exams.Dtos;

[Serializable]
public class TestPaperSectionGetListInput : PagedAndSortedResultRequestDto
{
    public Guid? TestPaperId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 大题描述(可用于保存阅读理解题、论述题、组合题等各种复杂题型的题干内容)
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 题目数量
    /// </summary>
    public int? QuestionCount { get; set; }

    /// <summary>
    /// 大题题目总分
    /// </summary>
    public decimal? TotalScore { get; set; }

    /// <summary>
    /// 序号
    /// </summary>
    public int? Sort { get; set; }
    
}