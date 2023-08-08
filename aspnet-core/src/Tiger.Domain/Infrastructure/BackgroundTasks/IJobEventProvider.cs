using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;

namespace Tiger.Infrastructure.BackgroundTasks
{
    /// <summary>
    /// 任务事件提供者
    /// </summary>
    public interface IJobEventProvider
    {
        /// <summary>
        /// 返回所有任务事件注册接口
        /// </summary>
        /// <returns></returns>
        IReadOnlyCollection<IJobEvent> GetAll();
    }
}
