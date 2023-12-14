using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Exams.Dtos;

/// <summary>
/// 试卷评委表        <remarks>    评卷人只有关联了试卷才能改卷（默认只有超管能改卷）    </remarks>
/// </summary>
[Serializable]
public class TestPaperJudgeDto : CreationAuditedEntityDto<Guid>
{
    /// <summary>
    /// 试卷Id
    /// </summary>
    public int TestPaperId { get; set; }

    /// <summary>
    /// 评卷人Id
    /// </summary>
    public Guid JudgeId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// 组织Id
    /// </summary>
    public Guid OrganizationUnitId { get; set; }
}