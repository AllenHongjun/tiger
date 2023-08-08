using Volo.Abp.DependencyInjection;

namespace Tiger.Infrastructure.BackgroundTasks.Activities;

public abstract class JobActionDefinitionProvider : IJobActionDefinitionProvider, ITransientDependency
{
    public abstract void Define(IJobActionDefinitionContext context);
}
