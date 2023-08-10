using Tiger.Infrastructure.BackgroundTasks.Abstractions.Localization;
using Volo.Abp.Localization;

namespace Tiger.Infrastructure.BackgroundTasks.Jobs;

internal static class LocalizableStatic
{
    public static ILocalizableString Create(string name)
    {
        return LocalizableString.Create<BackgroundTasksResource>(name);
    }
}
