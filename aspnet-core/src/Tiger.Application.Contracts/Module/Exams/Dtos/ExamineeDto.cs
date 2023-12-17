using JetBrains.Annotations;
using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Exams.Dtos;

/// <summary>
/// 考试人员表(应试人；参加考试者)
/// </summary>
[Serializable]
public class ExamineeDto : CreationAuditedEntityDto<Guid>
{

    /// <summary>
    /// 考生Id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string FullName { get; set; }

    [CanBeNull]
    public virtual string Name { get; set; }

    /// <summary>
    /// Gets or sets the Surname for the user.
    /// </summary>
    [CanBeNull]
    public virtual string Surname { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// 考生所属组织名称
    /// </summary>
    public string OrganizationUnitName { get; set; }
}