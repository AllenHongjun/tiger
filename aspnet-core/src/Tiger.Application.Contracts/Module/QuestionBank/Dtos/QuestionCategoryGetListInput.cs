using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.QuestionBank.Dtos;

[Serializable]
public class QuestionCategoryGetListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get;set; }

    public Guid? ParentId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 封面
    /// </summary>
    public string? Cover { get; set; }

    /// <summary>
    /// Hierarchical Code of this QuestionCategory.        Example: "00001.00042.00005".        This is a unique code for an QuestionCategory.        It's changeable if QC hierarchy is changed.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 是否显示
    /// </summary>
    public bool? Enable { get; set; }

    /// <summary>
    /// 是否公开
    /// </summary>
    public bool? IsPublic { get; set; }
}