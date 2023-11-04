using System;
using System.ComponentModel.DataAnnotations;
using Tiger.Module.System.Platform.Datas;
using Volo.Abp.Validation;

namespace Tiger.Module.Schools.Dtos;

[Serializable]
public class CreateUpdateSchoolDto
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(SchoolConsts), nameof(SchoolConsts.MaxNameLength))]
    public string Name { get; set; }

    /// <summary>
    /// 学校简称
    /// </summary>
    [DynamicStringLength(typeof(SchoolConsts), nameof(SchoolConsts.MaxShortNameLength))]
    public string ShortName { get; set; }

    /// <summary>
    /// 顺序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnable { get; set; }

    /// <summary>
    /// 编号
    /// </summary>
    [DynamicStringLength(typeof(SchoolConsts), nameof(SchoolConsts.MaxNameLength))]
    public string Number { get; set; }

    /// <summary>
    /// 授权截止时间
    /// </summary>
    [Required]
    public DateTime? ImpowerDate { get; set; }

    /// <summary>
    /// 最大人数
    /// </summary>
    [Required]
    public int MaxPerson { get; set; }

    /// <summary>
    /// 是否需要审核帖子
    /// </summary>
    public bool IsAudit { get; set; }

    /// <summary>
    /// Vip等级：1.免费客户 2.付费客户
    /// </summary>
    [Required]
    public VipLevel VipLevel { get; set; }

}