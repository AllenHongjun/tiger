//using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Tiger.Infrastructure.BackgroundTasks;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;
using Volo.Abp.DependencyInjection;

namespace Tiger.Infrastructure.BackgroundTasks.Quartz;

/// <summary>
/// Quartz简单适配器
/// </summary>
/// <typeparam name="TJobRunnable"></typeparam>
public class QuartzJobSimpleAdapter<TJobRunnable> : IJob, ITransientDependency
    where TJobRunnable : IJobRunnable
{
    protected IServiceScopeFactory ServiceScopeFactory { get; }

    public QuartzJobSimpleAdapter(
        IServiceScopeFactory serviceScopeFactory)
    {
        ServiceScopeFactory = serviceScopeFactory;
    }

    public async virtual Task Execute(IJobExecutionContext context)
    {
        using var scope = ServiceScopeFactory.CreateScope();
        var jobExecuter = scope.ServiceProvider.GetRequiredService<IJobRunnableExecuter>();

        var jobContext = new JobRunnableContext(
            typeof(TJobRunnable),
            scope.ServiceProvider,
            context.MergedJobDataMap.ToImmutableDictionary(),
            getCache: (key) => context.Get(key),
            setCache: context.Put,
            cancellationToken: context.CancellationToken);

        await jobExecuter.ExecuteAsync(jobContext);

        context.Result = jobContext.Result;
    }
}
