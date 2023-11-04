using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Schools.Dtos;

/// <summary>
/// 班级
/// </summary>
[Serializable]
public class ClassInfoDto : FullAuditedEntityDto<Guid>
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 学校
    /// </summary>
    public Guid SchoolId { get; set; }

    /// <summary>
    /// 顺序
    /// </summary>
    public int Sorting { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnable { get; set; }
    
}