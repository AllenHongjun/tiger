using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.BackgroundTasks.Abstractions.Enum
{

    /// <summary>
    /// 任务执行状态
    /// </summary>
    public enum JobActionType
    {
        Failed = -1,

        Successed = 0,

        Completed = 1,
    }
}
