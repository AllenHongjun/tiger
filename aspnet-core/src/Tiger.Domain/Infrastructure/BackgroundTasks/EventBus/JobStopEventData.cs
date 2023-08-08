using System;
using Volo.Abp.EventBus;

namespace Tiger.Infrastructure.BackgroundTasks.EventBus;

[Serializable]
[EventName("abp.background-tasks.job.stop")]
public class JobStopEventData : JobEventData
{
}
