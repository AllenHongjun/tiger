using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.BackgroundWorker;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace Tiger.Infrastructure.BackgroundWorker
{
    public class CrystalQuartzLogWorker: QuartzBackgroundWorkerBase
    {
        public CrystalQuartzLogWorker()
        {
        }


        /// <summary>
        /// 测试定时任务功能。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"Greetings from HelloJob!  执行时间：{DateTime.Now.ToString()}");
            //Logger.LogInformation($"Executed CrystalQuartzLogWorker..!  执行时间：{DateTime.Now.ToString()}");
            return Task.CompletedTask;
        }
    }
}
