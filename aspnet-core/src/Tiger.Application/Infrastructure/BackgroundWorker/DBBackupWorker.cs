using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace Tiger.Infrastructure.BackgroundWorker
{
    public class DBBackupWorker : QuartzBackgroundWorkerBase
    {
        public DBBackupWorker()
        {
            //JobDetail = JobBuilder.Create<DailySalesWorker>()
            //    .WithIdentity("DB_Backup", "Maintenance")
            //    .Build();

            //Trigger = TriggerBuilder.Create()
            //    .WithIdentity("db_trigger", "Maintenance")
            //    .ForJob(JobDetail)
            //    .StartNow()
            //    .WithCronSchedule("0 /1 * ? * *")
            //    .Build();

        }

        public override Task Execute(IJobExecutionContext context)
        {   
            // 查询数据库

            // 多线程执行 

            // 调用领域层功能

            // 禁止调用服务层



            Logger.LogInformation("Executed DBBackupWorker..!");
            return Task.CompletedTask;
        }
    }
}
