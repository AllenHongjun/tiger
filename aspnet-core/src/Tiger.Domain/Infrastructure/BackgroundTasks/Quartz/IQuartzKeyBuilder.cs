using Quartz;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;

namespace LINGYUN.Abp.BackgroundTasks.Quartz;

public interface IQuartzKeyBuilder
{
    JobKey CreateJobKey(JobInfo jobInfo);

    TriggerKey CreateTriggerKey(JobInfo jobInfo);
}
