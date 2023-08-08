using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;

namespace Tiger.Infrastructure.BackgroundTasks
{
    public interface IJobPublisher
    {
        Task<bool> PublishAsync(JobInfo job, CancellationToken cancellationToken = default);
    }
}
