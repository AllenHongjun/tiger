using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.BackgroundTasks.Abstractions.Enum
{
    /// <summary>
    /// 任务类型
    /// </summary>
    public enum JobType
    {

        /// <summary>
        /// 一次性的
        /// </summary>
        Once = 0,

        /// <summary>
        /// 周期性的
        /// </summary>
        Period = 1,

        /// <summary>
        /// 持续性的
        /// </summary>
        Persistent = 2,
    }
}
