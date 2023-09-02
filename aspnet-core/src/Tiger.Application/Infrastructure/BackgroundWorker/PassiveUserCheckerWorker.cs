using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Threading;
using Volo.Abp.Users;

namespace Tiger.Infrastructure.BackgroundWorker
{
    /// <summary>
    /// 设置用户为不活跃用户(如果用户最近30天未登录应用程序)
    /// </summary>
    public class PassiveUserCheckerWorker : QuartzBackgroundWorkerBase, ISingletonDependency
    {

        public PassiveUserCheckerWorker()
        {   
            JobDetail = JobBuilder.Create<PassiveUserCheckerWorker>()
                .WithIdentity("Passive User Checker", "Report")
                .Build();

            Trigger = TriggerBuilder.Create()
                .WithIdentity("puc_trigger", "Report")
                .ForJob(JobDetail)
                .StartNow()
                .WithCronSchedule("0 0 0/3 ? * *")
                .Build();
            
        }

        public override async Task Execute(IJobExecutionContext context)
        {
            Logger.LogInformation("Starting: Setting status of inactive users...");

            //Resolve dependencies
            // 最好使用 PeriodicBackgroundWorkerContext 解析依赖 而不是构造函数. 因为 AsyncPeriodicBackgroundWorkerBase 使用 IServiceScope 在你的任务执行结束时会对其 disposed.

            //// 解析数据库的类 调用数据库的方法
            ////Resolve dependencies 解析依赖关系
            var userRepository = ServiceProvider.GetService<IIdentityUserRepository>();
            var user = await userRepository.FindByNormalizedUserNameAsync("admin");

            

            Logger.LogInformation($"user info -- {user.Name}");

            ////Do the work
            //await userRepository.UpdateInactiveUserStatusesAsync();

            Logger.LogInformation("Completed: Setting status of inactive users...");
            Logger.LogInformation($"Completed: user.Name...{user.Name}");
            return ;
        }

        


        /*
            集群部署问题：
            专门有一台服务器来运行后台服务
            使用上面提到的 AbpBackgroundWorkerOptions 禁用其他的后台工作者系统,只保留一个实例.
            所有的应用程序都禁用后台工作者系统,创建一个特殊的应用程序在一个服务上运行执行工作者.
         
         */
    }

}
