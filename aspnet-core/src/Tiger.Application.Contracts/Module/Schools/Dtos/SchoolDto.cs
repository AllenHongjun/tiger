using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Schools.Dtos;

/// <summary>
/// 学校管理
/// </summary>
[Serializable]
public class SchoolDto : FullAuditedEntityDto<Guid>
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 学校简称
    /// </summary>
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
    public string Number { get; set; }

    /// <summary>
    /// 授权截止时间
    /// </summary>
    public DateTime? ImpowerDate { get; set; }

    /// <summary>
    /// 最大人数
    /// </summary>
    public int MaxPerson { get; set; }

    /// <summary>
    /// 是否需要审核帖子
    /// </summary>
    public bool IsAudit { get; set; }

    /// <summary>
    /// Vip等级：1.免费客户 2.付费客户
    /// </summary>
    public VipLevel VipLevel { get; set; }

}