﻿using System;
using Tiger.Infrastructure.BackgroundTasks.Abstractions.Enum;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.TaskManagement.Dtos;

public class BackgroundJobInfoGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 其他过滤条件
    /// </summary>
    public string Filter { get; set; }
    /// <summary>
    /// 任务名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 任务分组
    /// </summary>
    public string Group { get; set; }
    /// <summary>
    /// 任务类型
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// 任务状态
    /// </summary>
    public JobStatus? Status { get; set; }
    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? BeginTime { get; set; }
    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
    /// <summary>
    /// 上次起始触发时间
    /// </summary>
    public DateTime? BeginLastRunTime { get; set; }
    /// <summary>
    /// 上次截止触发时间
    /// </summary>
    public DateTime? EndLastRunTime { get; set; }
    /// <summary>
    /// 起始创建时间
    /// </summary>
    public DateTime? BeginCreationTime { get; set; }
    /// <summary>
    /// 截止创建时间
    /// </summary>
    public DateTime? EndCreationTime { get; set; }
    /// <summary>
    /// 是否已放弃任务
    /// </summary>
    public bool? IsAbandoned { get; set; }
    /// <summary>
    /// 任务类型
    /// </summary>
    public JobType? JobType { get; set; }
    /// <summary>
    /// 优先级
    /// </summary>
    public JobPriority? Priority { get; set; }
    /// <summary>
    /// 作业来源
    /// </summary>
    public JobSource? Source { get; set; }
}
