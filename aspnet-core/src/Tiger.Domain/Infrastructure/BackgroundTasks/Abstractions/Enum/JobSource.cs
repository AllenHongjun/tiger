using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.BackgroundTasks.Abstractions.Enum
{
    /// <summary>
    /// 任务来源
    /// </summary>
    public enum JobSource
    {
        /// <summary>
        /// 未定义
        /// </summary>
        None = -1,

        /// <summary>
        /// 用户
        /// </summary>
        User = 0,

        /// <summary>
        /// 系统内置
        /// </summary>
        System = 10
    }
}
