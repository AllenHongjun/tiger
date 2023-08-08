using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Tiger.Infrastructure.BackgroundTasks.Activities;

public interface IJobExecutedProvider
{
    Task NotifyErrorAsync([NotNull] JobActionExecuteContext context);
    Task NotifySuccessAsync([NotNull] JobActionExecuteContext context);
    Task NotifyComplateAsync([NotNull] JobActionExecuteContext context);
}
