using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Exams.Dtos;

[Serializable]
public class ExamModifyTimeGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 考试Id
    /// </summary>
    public Guid? ExamId { get; set; }

    /// <summary>
    /// 考试推迟时间：单位分钟
    /// </summary>
    public int? DelayTime { get; set; }

    /// <summary>
    /// 考试延长时间：单位分钟
    /// </summary>
    public int? ExtendTime { get; set; }

    /// <summary>
    /// 组织Id
    /// </summary>
    public Guid? OrganizationUnitId { get; set; }

    /// <summary>
    /// 考生Id
    /// </summary>
    public Guid? ExamineeId { get; set; }
}