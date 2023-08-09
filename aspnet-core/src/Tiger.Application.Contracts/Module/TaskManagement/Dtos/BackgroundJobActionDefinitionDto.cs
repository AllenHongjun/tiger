﻿using System.Collections.Generic;
using Tiger.Infrastructure.BackgroundTasks.Abstractions.Enum;

namespace Tiger.Module.TaskManagement.Dtos;

public class BackgroundJobActionDefinitionDto
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    public JobActionType Type { get; set; }
    /// <summary>
    /// 显示名称
    /// </summary>
    public string DisplayName { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// 参数列表
    /// </summary>
    public IList<BackgroundJobActionParamterDto> Paramters { get; set; }
}
