using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace Tiger.Infrastructure.BackgroundWorker
{
    public class DailySalesWorker : QuartzBackgroundWorkerBase
    {
        public DailySalesWorker()
        {
            //JobDetail = JobBuilder.Create<DailySalesWorker>()
            //    .WithIdentity("Daily Sales", "Report")
            //    .Build();

            //Trigger = TriggerBuilder.Create()
            //    .WithIdentity("ds_trigger", "Report")
            //    .ForJob(JobDetail)
            //    .StartNow()
            //    .WithCronSchedule("0 0 0/3 ? * *")
            //    .Build();
        }


        /// <summary>
        /// 测试定时任务功能。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task Execute(IJobExecutionContext context)
        {
            //Console.WriteLine($"Greetings from DailySalesWorker!  执行时间：{DateTime.Now.ToString()}");
            //Logger.LogInformation($"Executed DailySalesWorker..!  执行时间：{DateTime.Now.ToString()}");
            return Task.CompletedTask;
        }
    }
}
