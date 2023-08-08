using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.BackgroundTasks.Abstractions
{
    public class JobEventContext
    {
        public IServiceProvider ServiceProvider { get; }
        public JobEventData EventData { get; }

        public JobEventContext(
            IServiceProvider serviceProvider,
            JobEventData jobEventData)
        {
            ServiceProvider = serviceProvider;
            EventData = jobEventData;
        }
    }
}
