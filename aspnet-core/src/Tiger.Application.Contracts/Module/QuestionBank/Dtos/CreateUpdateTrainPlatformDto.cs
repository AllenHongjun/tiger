using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Tiger.Module.QuestionBank.Dtos;

[Serializable]
public class CreateUpdateTrainPlatformDto
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(TrainPlatformConsts), nameof(TrainPlatformConsts.MaxNameLength))]
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [DynamicStringLength(typeof(TrainPlatformConsts), nameof(TrainPlatformConsts.MaxDescriptionLength))]
    public string Description { get; set; }

    /// <summary>
    /// logo图标
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(TrainPlatformConsts), nameof(TrainPlatformConsts.MaxIconLength))]
    public string Icon { get; set; }

    /// <summary>
    /// 实训跳转链接
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(TrainPlatformConsts), nameof(TrainPlatformConsts.MaxUrlLength))]
    public string Url { get; set; }

    /// <summary>
    /// token校验码
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(TrainPlatformConsts), nameof(TrainPlatformConsts.MaxCheckCodeLength))]
    public string CheckCode { get; set; }

    /// <summary>
    /// Token传值方式：0、使用旧版Cookie；1、使用旧版Url Token；2、使用新版Url Token；
    /// </summary>
    public TokenType TokenType { get; set; }

    /// <summary>
    /// 序号
    /// </summary>
    public int Sorting { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enable { get; set; }
}