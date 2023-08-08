using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Tiger.Infrastructure.BackgroundTasks.Activities;

public abstract class JobExecutedProvider : IJobExecutedProvider
{
    public virtual Task NotifyComplateAsync([NotNull] JobActionExecuteContext context)
    {
        return Task.CompletedTask;
    }

    public virtual Task NotifyErrorAsync([NotNull] JobActionExecuteContext context)
    {
        return Task.CompletedTask;
    }

    public virtual Task NotifySuccessAsync([NotNull] JobActionExecuteContext context)
    {
        return Task.CompletedTask;
    }
}
