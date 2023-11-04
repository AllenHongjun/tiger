using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Schools.Dtos;

[Serializable]
public class ClassInfoGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 学校
    /// </summary>
    public Guid? SchoolId { get; set; }


    /// <summary>
    /// 是否启用
    /// </summary>
    public bool? IsEnable { get; set; }
    
}