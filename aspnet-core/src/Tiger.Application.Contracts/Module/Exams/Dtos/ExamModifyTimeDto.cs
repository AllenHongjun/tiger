using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Exams.Dtos;

/// <summary>
/// 考试时间调整表
/// </summary>
[Serializable]
public class ExamModifyTimeDto : FullAuditedEntityDto<Guid>
{
    /// <summary>
    /// 考试Id
    /// </summary>
    public Guid ExamId { get; set; }

    /// <summary>
    /// 考试推迟时间：单位分钟
    /// </summary>
    public int DelayTime { get; set; }

    /// <summary>
    /// 考试延长时间：单位分钟
    /// </summary>
    public int ExtendTime { get; set; }

    /// <summary>
    /// 组织Id
    /// </summary>
    public Guid OrganizationUnitId { get; set; }

    /// <summary>
    /// 考生Id
    /// </summary>
    public Guid ExamineeId { get; set; }

    /// <summary>
    /// 考试名称
    /// </summary>
    public string ExamName { get; set; }

    /// <summary>
    /// 组织名称
    /// </summary>
    public string OrganizationUnitName { get; set; }

}