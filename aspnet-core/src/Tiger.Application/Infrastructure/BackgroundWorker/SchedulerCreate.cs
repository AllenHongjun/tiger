using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.BackgroundWorker
{
    public static class SchedulerManage
    {
        public static IScheduler Create()
        {
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().Result;

            var job = JobBuilder.Create<CrystalQuartzLogWorker>()
                .WithIdentity("localJob", "default")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "default")
                .ForJob(job)
                .StartNow()
                .WithCronSchedule("0 /1 * ? * *")
                .Build();

            scheduler.ScheduleJob(job, trigger);

            scheduler.Start();
            return scheduler;
        }
    }
}
