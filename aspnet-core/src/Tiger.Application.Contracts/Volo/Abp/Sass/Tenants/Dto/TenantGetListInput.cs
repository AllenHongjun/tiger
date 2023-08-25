using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.Sass.Tenants;

public class TenantGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 查询关键子
    /// </summary>
    public string Filter { get; set; }

    /// <summary>
    /// 版本ID
    /// </summary>
    public Guid? EditionId { get; set; }

    /// <summary>
    /// 截止日期开始
    /// </summary>
    public DateTime? DisableBeginTime { get; set; }

    /// <summary>
    /// 截止日期结束
    /// </summary>
    public DateTime? DisableEndTime { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool? IsActive { get; set; }
}