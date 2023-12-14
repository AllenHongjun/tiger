using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace Tiger.Module.Exams.Dtos;

[Serializable]
public class CreateUpdateExamineeDto
{
    /// <summary>
    /// 考试Id
    /// </summary>
    public Guid ExamId { get; set; }

    /// <summary>
    /// 考生Id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxUserNameLength))]
    public string UserName { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxSurnameLength))]
    public string FullName { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxEmailLength))]
    public string Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPhoneNumberLength))]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// 考生所属组织Id
    /// </summary>
    public Guid OrganizationUnitId { get; set; }

    /// <summary>
    /// 考生所属组织名称
    /// </summary>
    public string OrganizationUnitName { get; set; }
}