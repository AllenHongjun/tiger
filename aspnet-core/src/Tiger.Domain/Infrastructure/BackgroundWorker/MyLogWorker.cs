/**
 * 类    名：MyLogWorker   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/12 8:46:47       
 * 说    明: 
 * 
 */

using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace Tiger.BackgroundWorker
{   

    /// <summary>
    /// Demo 定时任务
    /// </summary>
    public class MyLogWorker : QuartzBackgroundWorkerBase
    {
        private readonly ILogger _logger;
        public MyLogWorker(ILogger<MyLogWorker> logger)
        {
            _logger = logger;
            //默认后台工作者会在应用程序启动时自动添加到 BackgroundWorkerManager,如果你想要手动添加,可以将 AutoRegister 属性值设置为 false:
            //AutoRegister = false;
            // nameof(MyLogWorker)
            JobDetail = JobBuilder.Create<MyLogWorker>().WithIdentity("MyLogWorker", "group1").Build();
            //Trigger = TriggerBuilder.Create().WithIdentity(nameof(MyLogWorker)).StartNow().Build();

            // 在示例中我们定义了工作者执行间隔为10分钟,并且设置 WithMisfireHandlingInstructionIgnoreMisfires ,另外自定义 ScheduleJob 仅当工作者不存在时向quartz添加调度作业.
            Trigger = TriggerBuilder.Create().WithIdentity("MyLogWorker", "group1")
                .WithSimpleSchedule(  // //使用SimpleTrigger
                    s => s.WithIntervalInMinutes(1) //每隔一分钟执行一次
                    .RepeatForever() //一直执行，奔腾到老不停歇
                    .WithMisfireHandlingInstructionIgnoreMisfires()
                )
                .Build();


            //在示例中我们定义了工作者执行间隔为10分钟,并且设置 WithMisfireHandlingInstructionIgnoreMisfires ,另外自定义 ScheduleJob 仅当工作者不存在时向quartz添加调度作业.
            ScheduleJob = async scheduler =>
            {
                if (!await scheduler.CheckExists(JobDetail.Key))
                {
                    await scheduler.ScheduleJob(JobDetail, Trigger);
                }
            };
            _logger=logger;
        }

        /// <summary>
        /// 测试定时任务功能。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Executed MyLogWorker..!  执行时间：{DateTime.Now.ToString()}");
            return Task.CompletedTask;
        }
    }
}
