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
    public class NullJobPublisher : IJobPublisher, ISingletonDependency
    {
        public static readonly NullJobPublisher Instance = new NullJobPublisher();
        public Task<bool> PublishAsync(JobInfo job, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(false);
        }
    }


}
