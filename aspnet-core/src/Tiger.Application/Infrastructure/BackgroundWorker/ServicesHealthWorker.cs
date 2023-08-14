using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace Tiger.Infrastructure.BackgroundWorker
{
    public class ServicesHealthWorker : QuartzBackgroundWorkerBase
    {
        public ServicesHealthWorker()
        {
            //JobDetail = JobBuilder.Create<DailySalesWorker>()
            //    .WithIdentity("Services Health", "Report")
            //    .Build();

            //Trigger = TriggerBuilder.Create()
            //    .WithIdentity("sh_trigger", "Report")
            //    .ForJob(JobDetail)
            //    .StartNow()
            //    .WithCronSchedule("0 0 0/3 ? * *")
            //    .Build();
        }

        public override Task Execute(IJobExecutionContext context)
        {
            Logger.LogInformation($"Executed ServicesHealthWorker..!  执行时间：{DateTime.Now.ToString()}");
            return Task.CompletedTask;
        }
    }
}
