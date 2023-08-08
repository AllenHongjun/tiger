using Tiger.Infrastructure.BackgroundTasks.Abstractions.Enum;
using Tiger.Infrastructure.BackgroundTasks.Abstractions.Localization;
using Tiger.Infrastructure.BackgroundTasks.Activities;
using Volo.Abp.Localization;

namespace Tiger.Infrastructure.BackgroundTasks.ExceptionHandling;

public class ExceptionHandlingJobActionDefinitionProvider : JobActionDefinitionProvider
{
    public override void Define(IJobActionDefinitionContext context)
    {
        context.Add(
            new JobActionDefinition(
                JobExecutedFailedProvider.Name,
                JobActionType.Failed,
                L("JobExceptionNotifier"),
                JobExecutedFailedProvider.Paramters,
                L("JobExceptionNotifier"))
            .WithProvider<JobExecutedFailedProvider>());
    }

    private static ILocalizableString L(string name)
    {
        return LocalizableString.Create<BackgroundTasksResource>(name);
    }
}
