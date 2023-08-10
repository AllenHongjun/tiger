using Quartz;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;
using Volo.Abp.DependencyInjection;

namespace Tiger.Infrastructure.BackgroundTasks.Quartz;

public class QuartzKeyBuilder : IQuartzKeyBuilder, ISingletonDependency
{
    public JobKey CreateJobKey(JobInfo jobInfo)
    {
        var name = jobInfo.Id;
        var group = jobInfo.TenantId.HasValue
            ? $"{jobInfo.TenantId}:{jobInfo.Group}"
            : $"Default:{jobInfo.Group}";

        return new JobKey(name, group);
    }

    public TriggerKey CreateTriggerKey(JobInfo jobInfo)
    {
        var name = jobInfo.Id;
        var group = jobInfo.TenantId.HasValue
            ? $"{jobInfo.TenantId}:{jobInfo.Group}"
            : $"Default:{jobInfo.Group}";

        return new TriggerKey(name, group);
    }
}
