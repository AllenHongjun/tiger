using System;

namespace Tiger.Module.QuestionBank.Dtos;

[Serializable]
public class CreateUpdateQuestionCategoryDto
{
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 封面
    /// </summary>
    public string Cover { get; set; }

    /// <summary>
    /// Hierarchical Code of this QuestionCategory.        Example: "00001.00042.00005".        This is a unique code for an QuestionCategory.        It's changeable if QC hierarchy is changed.
    /// </summary>
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