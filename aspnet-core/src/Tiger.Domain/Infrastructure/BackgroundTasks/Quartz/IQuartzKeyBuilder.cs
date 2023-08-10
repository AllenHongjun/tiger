using Quartz;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;

namespace Tiger.Infrastructure.BackgroundTasks.Quartz;

public interface IQuartzKeyBuilder
{
    JobKey CreateJobKey(JobInfo jobInfo);

    TriggerKey CreateTriggerKey(JobInfo jobInfo);
}
