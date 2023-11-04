using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Tiger.Module.Schools.Dtos;

[Serializable]
public class CreateUpdateClassInfoDto
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(SchoolConsts), nameof(SchoolConsts.MaxNameLength))]
    public string Name { get; set; }

    /// <summary>
    /// 学校
    /// </summary>
    [Required]
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