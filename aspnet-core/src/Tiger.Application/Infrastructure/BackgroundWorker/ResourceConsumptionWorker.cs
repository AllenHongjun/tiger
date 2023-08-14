using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace Tiger.Infrastructure.BackgroundWorker
{   
    /// <summary>
    /// 资源消耗
    /// </summary>
    public class ResourceConsumptionWorker : QuartzBackgroundWorkerBase
    {
        public ResourceConsumptionWorker()
        {
            //JobDetail = JobBuilder.Create<DailySalesWorker>()
            //    .WithIdentity("Resource Consumption", "Report")
            //    .Build();

            //Trigger = TriggerBuilder.Create()
            //    .WithIdentity("rc_trigger", "Report")
            //    .ForJob(JobDetail)
            //    .StartNow()
            //    .WithCronSchedule("0 0 0/3 ? * *")
            //    .Build();
        }

        public override Task Execute(IJobExecutionContext context)
        {
            Logger.LogInformation("Executed ResourceConsumptionWorker..!");
            return Task.CompletedTask;
        }
    }
}
