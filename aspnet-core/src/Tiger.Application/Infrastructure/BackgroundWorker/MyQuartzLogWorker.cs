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
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace Tiger.BackgroundWorker
{

    /// <summary>
    /// Demo 定时任务
    /// </summary>
    /// <remarks>
    /// 
    /// 一定要定义Trigger  和JobDetail 不然项目无法启动
    /// 如果数据库 的 Trigger状态是error就无法启动
    /// 不能和 abp自带的任务管理器一起使用 会无法运行
    /// 安装模块化的事项 那个模块需要什么作业 把作业放到对应的模块里面
    /// </remarks>
    public class MyQuartzLogWorker : QuartzBackgroundWorkerBase
    {

        public MyQuartzLogWorker()
        {
            ////默认后台工作者会在应用程序启动时自动添加到 BackgroundWorkerManager,如果你想要手动添加,可以将 AutoRegister 属性值设置为 false:
            //// nameof(MyLogWorker)
            //JobDetail = JobBuilder.Create<MyQuartzLogWorker>().WithIdentity("MyLogWorker", "group1").Build();
            //Trigger = TriggerBuilder.Create()
            //    .WithIdentity(nameof(MyQuartzLogWorker))
            //    .StartNow().Build();

            // 在示例中我们定义了工作者执行间隔为10分钟,并且设置 WithMisfireHandlingInstructionIgnoreMisfires ,另外自定义 ScheduleJob 仅当工作者不存在时向quartz添加调度作业.
            JobDetail = JobBuilder.Create<MyQuartzLogWorker>()
                .WithIdentity(nameof(MyQuartzLogWorker)).Build();
            Trigger = TriggerBuilder.Create()
                .WithIdentity(nameof(MyQuartzLogWorker))
                .WithSimpleSchedule(s => s.WithIntervalInSeconds(4).RepeatForever()
                .WithMisfireHandlingInstructionIgnoreMisfires()).StartNow().Build();

            ScheduleJob = async scheduler =>
            {
                if (!await scheduler.CheckExists(JobDetail.Key))
                {
                    await scheduler.ScheduleJob(JobDetail, Trigger);
                }
            };
        }
        

        /// <summary>
        /// 测试定时任务功能。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task Execute(IJobExecutionContext context)
        {
            Logger.LogInformation($"Executed MyQuartzLogWorker..!  执行时间：{DateTime.Now.ToString()}");
            return Task.CompletedTask;
        }
    }
}
