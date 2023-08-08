using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;

namespace LINGYUN.Abp.BackgroundTasks.Quartz;

[DisallowConcurrentExecution]
public class QuartzJobConcurrentAdapter<TJobRunnable> : QuartzJobSimpleAdapter<TJobRunnable>
    where TJobRunnable : IJobRunnable
{
    public QuartzJobConcurrentAdapter(IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
    }
}
