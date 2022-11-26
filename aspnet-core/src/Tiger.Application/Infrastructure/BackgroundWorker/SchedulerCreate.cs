using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Infrastructure.BackgroundWorker.DogApi;

namespace Tiger.Infrastructure.BackgroundWorker
{
    /// <summary>
    /// Scheduler管理器
    /// </summary>
    public static class SchedulerManager
    {


        /// <summary>
        /// 初始化创建Scheduler
        /// </summary>
        /// <returns></returns>
        public static IScheduler Create()
        {
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().Result;

            CreateCompressLogsJob(scheduler);
            CreateDBBackupJob(scheduler);


            CreateDailySalesJob(scheduler);

            CreateDogApiJob(scheduler);



            scheduler.Start();
            return scheduler;
        }


        #region Maintenance 系统任务分组
        
        
        /// <summary>
        /// 压缩日志任务
        /// </summary>
        /// <param name="scheduler"></param>
        /// <returns></returns>
        private static IScheduler CreateCompressLogsJob(IScheduler scheduler)
        {
            var job = JobBuilder.Create<CrystalQuartzLogWorker>()
                .WithIdentity("CompressLogs", "Maintenance")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("log_trigger", "Maintenance")
                .ForJob(job)
                .StartNow()
                .WithCronSchedule("0 /1 * ? * *")
                .Build();

            var trigger2 = TriggerBuilder.Create()
                .WithIdentity("log_trigger2", "Maintenance")
                .ForJob(job)
                .StartNow()
                .WithCronSchedule("0 0/3 * * * ? ")
                .Build();
            
            scheduler.ScheduleJob(job, trigger);
            // 添加第二个触发器
            scheduler.ScheduleJob(trigger2);
            return scheduler;
        }

        /// <summary>
        /// 数据库备份任务
        /// </summary>
        /// <param name="scheduler"></param>
        /// <returns></returns>
        private static IScheduler CreateDBBackupJob(IScheduler scheduler)
        {
            var job = JobBuilder.Create<CrystalQuartzLogWorker>()
                .WithIdentity("DB_Backup", "Maintenance")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("db_trigger", "Maintenance")
                .ForJob(job)
                .StartNow()
                .WithCronSchedule("0 /1 * ? * *")
                .Build();

            scheduler.ScheduleJob(job, trigger);
            return scheduler;
        }
        #endregion

        #region 报表 Report任务
        
        /// <summary>
        /// 每日销售报表
        /// </summary>
        /// <param name="scheduler"></param>
        /// <returns></returns>
        private static IScheduler CreateDailySalesJob(IScheduler scheduler)
        {
            var job = JobBuilder.Create<CrystalQuartzLogWorker>()
                .WithIdentity("Daily Sales", "Report")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("ds_trigger", "Report")
                .ForJob(job)
                .StartNow()
                .WithCronSchedule("0/3 * * ? * *")
                .Build();

            scheduler.ScheduleJob(job, trigger);
            return scheduler;
        }
        #endregion


        private static IScheduler CreateDogApiJob(IScheduler scheduler)
        {
            var job = JobBuilder.Create<ListAllBreedsWorker>()
                .WithIdentity("Dog Api", "Api")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("ms_trigger", "Api")
                .ForJob(job)
                .StartNow()
                .WithCronSchedule("0/10 * * ? * *")
                .Build();

            scheduler.ScheduleJob(job, trigger);
            return scheduler;
        }

    }
}
