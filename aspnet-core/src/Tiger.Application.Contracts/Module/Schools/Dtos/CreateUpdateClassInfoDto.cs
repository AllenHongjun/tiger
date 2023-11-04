using System;

namespace Tiger.Module.Schools.Dtos;

[Serializable]
public class CreateUpdateClassInfoDto
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