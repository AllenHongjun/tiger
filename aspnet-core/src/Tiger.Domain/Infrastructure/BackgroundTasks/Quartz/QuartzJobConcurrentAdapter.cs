using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;

namespace Tiger.Infrastructure.BackgroundTasks.Quartz;

/// <summary>
/// QuartzJob并发适配器
/// </summary>
/// <typeparam name="TJobRunnable"></typeparam>
[DisallowConcurrentExecution]
public class QuartzJobConcurrentAdapter<TJobRunnable> : QuartzJobSimpleAdapter<TJobRunnable>
    where TJobRunnable : IJobRunnable
{
    public QuartzJobConcurrentAdapter(IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
    }
}
