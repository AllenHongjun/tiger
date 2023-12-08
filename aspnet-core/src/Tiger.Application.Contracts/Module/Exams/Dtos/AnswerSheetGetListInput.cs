using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Exams.Dtos;

[Serializable]
public class AnswerSheetGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 查询关键字
    /// </summary>
    public string Filter { get; set; }

    /// <summary>
    /// 创建开始时间
    /// </summary>
    public DateTime? CreateStartTime { get; set; }

    /// <summary>
    /// 创建结束时间
    /// </summary>
    public DateTime? CreateEndTime { get; set; }

    /// <summary>
    /// 考试Id
    /// </summary>
    public Guid? ExamId { get; set; }

    /// <summary>
    /// 组织Id
    /// </summary>
    public Guid? OrganizationUnitId { get; set; }

    /// <summary>
    /// 是否及格
    /// </summary>
    public bool? IsPass { get; set; }

    /// <summary>
    /// 答卷状态
    /// </summary>
    public AnswerSheetStatus? AnswerSheetStatus { get; set;}
}