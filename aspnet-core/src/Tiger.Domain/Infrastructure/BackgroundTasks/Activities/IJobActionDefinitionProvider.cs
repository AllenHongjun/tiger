namespace Tiger.Infrastructure.BackgroundTasks.Activities;

public interface IJobActionDefinitionProvider
{
    void Define(IJobActionDefinitionContext context);
}
