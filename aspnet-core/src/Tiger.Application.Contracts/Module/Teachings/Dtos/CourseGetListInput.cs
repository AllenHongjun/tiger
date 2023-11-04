using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Teachings.Dtos;

[Serializable]
public class CourseGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 课程类型：1、公开课程  2、私有课程
    /// </summary>
    public CourseType? Type { get; set; }

    /// <summary>
    /// 课程名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 课程描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 封面图片
    /// </summary>
    public string? Cover { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool? Enable { get; set; }

    /// <summary>
    /// 顺序
    /// </summary>
    public int? Sorting { get; set; }
}