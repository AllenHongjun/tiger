using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Exams.Dtos;

[Serializable]
public class ExamineeGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 考试Id
    /// </summary>
    public Guid? ExamId { get; set; }

    /// <summary>
    /// 考生Id
    /// </summary>
    public Guid? UserId { get; set; }

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
    /// 考生所属组织Id
    /// </summary>
    public Guid? OrganizationUnitId { get; set; }

    /// <summary>
    /// 考生所属组织名称
    /// </summary>
    public string? OrganizationUnitName { get; set; }
}