using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;
using Volo.Abp.DependencyInjection;

namespace Tiger.Infrastructure.BackgroundTasks
{
    [Dependency(TryRegister = true)]
    public class NullJobScheduler : IJobScheduler, ISingletonDependency
    {
        public static readonly IJobScheduler Instance = new NullJobScheduler();
        public Task<bool> ExistsAsync(JobInfo job, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(false);
        }

        public Task PauseAsync(JobInfo job, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task<bool> QueueAsync(JobInfo job, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(false);
        }

        public Task QueuesAsync(IEnumerable<JobInfo> jobs, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task<bool> RemoveAsync(JobInfo job, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(false);
        }

        public Task ResumeAsync(JobInfo job, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task<bool> ShutdownAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(false);
        }

        public Task<bool> StartAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(false);
        }

        public Task<bool> StopAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(false);
        }

        public Task TriggerAsync(JobInfo job, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }

}
