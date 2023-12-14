using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace Tiger.Module.Exams.Dtos;

[Serializable]
public class CreateUpdateTestPaperJudgeDto
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
    /// 组织Id
    /// </summary>
    public Guid OrganizationUnitId { get; set; }
}