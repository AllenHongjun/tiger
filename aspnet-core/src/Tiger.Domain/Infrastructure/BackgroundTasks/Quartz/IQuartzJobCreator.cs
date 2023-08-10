using Quartz;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;

namespace Tiger.Infrastructure.BackgroundTasks.Quartz;

public interface IQuartzJobCreator
{
#nullable enable
    IJobDetail? CreateJob(JobInfo job);

    ITrigger? CreateTrigger(JobInfo job);
#nullable disable
}
