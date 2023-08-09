﻿using System;
using System.ComponentModel.DataAnnotations;
using Tiger.Infrastructure.BackgroundTasks.Abstractions.Enum;
using Volo.Abp.Validation;

namespace Tiger.Module.TaskManagement.Dtos;

public class BackgroundJobInfoCreateDto : BackgroundJobInfoCreateOrUpdateDto
{
    /// <summary>
    /// 任务名称
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(BackgroundJobInfoConsts), nameof(BackgroundJobInfoConsts.MaxNameLength))]
    public string Name { get; set; }
    /// <summary>
    /// 任务分组
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(BackgroundJobInfoConsts), nameof(BackgroundJobInfoConsts.MaxGroupLength))]
    public string Group { get; set; }
    /// <summary>
    /// 任务类型
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(BackgroundJobInfoConsts), nameof(BackgroundJobInfoConsts.MaxTypeLength))]
    public string Type { get; set; }

    [DynamicStringLength(typeof(BackgroundJobInfoConsts), nameof(BackgroundJobInfoConsts.MaxNodeNameLength))]
    public string NodeName { get; set; }
    /// <summary>
    /// 开始时间
    /// </summary>
    [Required]
    public DateTime BeginTime { get; set; }
    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
    /// <summary>
    /// 作业来源
    /// </summary>
    public JobSource Source { get; set; } = JobSource.User;
}
