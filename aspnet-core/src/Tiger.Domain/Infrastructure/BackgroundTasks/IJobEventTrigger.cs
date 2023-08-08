using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;

namespace Tiger.Infrastructure.BackgroundTasks
{
    public interface IJobEventTrigger
    {
        Task OnJobBeforeExecuted(JobEventContext context);

        Task OnJobAfterExecuted(JobEventContext context);
    }
}
