using Hangfire.Common;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace Tiger.Infrastructure.BackgroundWorker
{
    public class MyLogWorker : QuartzBackgroundWorkerBase
    {
        public MyLogWorker()
        {
            JobDetail = JobBuilder.Create<MyLogWorker>()
                .WithIdentity("CompressLogs", "Maintenance")
                .Build();

            Trigger = TriggerBuilder.Create()
                .WithIdentity("log_trigger", "Maintenance")
                .ForJob(JobDetail)
                .StartNow()
                .WithCronSchedule("0 0/15 * ? * *")
                .Build();

        }

        public override Task Execute(IJobExecutionContext context)
        {
            Logger.LogInformation("Executed MyLogWorker..!");
            return Task.CompletedTask;
        }
    }

}
