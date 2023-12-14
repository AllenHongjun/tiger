using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.QuestionBank.Dtos;

[Serializable]
public class TrainPlatformGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 查询关键字
    /// </summary>
    public string? filter { get; set; }

    /// <summary>
    /// 创建开始时间
    /// </summary>
    public DateTime? CreateStartTime { get; set; }

    /// <summary>
    /// 创建结束时间
    /// </summary>
    public DateTime? CreateEndTime { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// logo图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 实训跳转链接
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// token校验码
    /// </summary>
    public string? CheckCode { get; set; }

    /// <summary>
    /// Token传值方式：0、使用旧版Cookie；1、使用旧版Url Token；2、使用新版Url Token；
    /// </summary>
    public TokenType? TokenType { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool? Enable { get; set; }
}