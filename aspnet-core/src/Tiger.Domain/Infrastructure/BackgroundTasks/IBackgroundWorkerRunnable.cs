using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;
using Volo.Abp.BackgroundWorkers;

namespace Tiger.Infrastructure.BackgroundTasks
{
    public interface IBackgroundWorkerRunnable : IJobRunnable
    {
#nullable enable
        JobInfo? BuildWorker(IBackgroundWorker worker);
#nullable disable
    }
}
