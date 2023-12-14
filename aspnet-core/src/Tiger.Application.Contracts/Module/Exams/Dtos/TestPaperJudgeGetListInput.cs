using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Exams.Dtos;

[Serializable]
public class TestPaperJudgeGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 试卷Id
    /// </summary>
    public int? TestPaperId { get; set; }

    /// <summary>
    /// 评卷人Id
    /// </summary>
    public Guid? JudgeId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// 组织Id
    /// </summary>
    public Guid? OrganizationUnitId { get; set; }
}