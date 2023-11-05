using System;
using System.ComponentModel.DataAnnotations;
using Tiger.Module.Exams;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Tiger.Module.QuestionBank.Dtos;

/// <summary>
/// 题目分类
/// </summary>
[Serializable]
public class QuestionCategoryDto : FullAuditedEntityDto<Guid>
{
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(QuestionCategoryConsts), nameof(QuestionCategoryConsts.MaxNameLength))]
    public string Name { get; set; }

    /// <summary>
    /// 封面
    /// </summary>
    [DynamicStringLength(typeof(QuestionCategoryConsts), nameof(QuestionCategoryConsts.MaxCoverLength))]
    public string Cover { get; set; }

    /// <summary>
    /// Hierarchical Code of this QuestionCategory.        Example: "00001.00042.00005".        This is a unique code for an QuestionCategory.        It's changeable if QC hierarchy is changed.
    /// </summary>
    [DynamicStringLength(typeof(QuestionCategoryConsts), nameof(QuestionCategoryConsts.MaxCodeLength))]
    public string Code { get; set; }

    /// <summary>
    /// 是否显示
    /// </summary>
    public bool Enable { get; set; }

    /// <summary>
    /// 顺序
    /// </summary>
    public int Sorting { get; set; }

    /// <summary>
    /// 是否公开
    /// </summary>
    public bool IsPublic { get; set; }
}