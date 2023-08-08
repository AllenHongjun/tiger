using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Infrastructure.BackgroundTasks.Abstractions
{
    /// <summary>
    /// 任务类需要实现此接口
    /// </summary>
    public interface IJobRunnable
    {
        Task ExecuteAsync(JobRunnableContext context);
    }
}
