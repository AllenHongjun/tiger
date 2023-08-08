using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Infrastructure.BackgroundTasks.Abstractions
{
    public interface IJobEvent
    {
        /// <summary>
        /// 任务启动前事件
        /// </summary>
        /// <param name="jobEventData"></param>
        /// <returns></returns>
        Task OnJobBeforeExecuted(JobEventContext context);
        /// <summary>
        /// 任务完成后事件
        /// </summary>
        /// <param name="jobEventData"></param>
        /// <returns></returns>
        Task OnJobAfterExecuted(JobEventContext context);
    }
}
