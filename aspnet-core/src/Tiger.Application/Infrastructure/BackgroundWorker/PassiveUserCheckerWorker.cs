using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Users;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Identity;
using Volo.Abp.Threading;
using Volo.Abp.Users;

namespace Tiger.Infrastructure.BackgroundWorker
{
    /// <summary>
    /// 设置用户为不活跃用户(如果用户最近30天未登录应用程序)
    /// </summary>
    public class PassiveUserCheckerWorker : AsyncPeriodicBackgroundWorkerBase
    {
        public PassiveUserCheckerWorker(
                AbpTimer timer,
                IServiceScopeFactory serviceScopeFactory
            ) : base(
                timer,
                serviceScopeFactory)
        {
            //AsyncPeriodicBackgroundWorkerBase 使用 AbpTimer(线程安全定时器)对象来确定时间段. 我们可以在构造函数中设置了Period 属性.
            Timer.Period = 600000; //10 minutes
        }

        /// <summary>
        /// 实现 DoWorkAsync 方法执行定期任务.
        /// </summary>
        /// <param name="workerContext"></param>
        /// <returns></returns>
        protected override async Task DoWorkAsync(
            PeriodicBackgroundWorkerContext workerContext)
        {
            Logger.LogInformation("Starting: Setting status of inactive users...");

            //Resolve dependencies
            // 最好使用 PeriodicBackgroundWorkerContext 解析依赖 而不是构造函数. 因为 AsyncPeriodicBackgroundWorkerBase 使用 IServiceScope 在你的任务执行结束时会对其 disposed.

            // 解析数据库的类 调用数据库的方法
            var userRepository = workerContext
            .ServiceProvider
                .GetRequiredService<IUserRepository<IdentityUser>>();
            await userRepository.GetListAsync();

            ////Do the work
            //await userRepository.UpdateInactiveUserStatusesAsync();

            Logger.LogInformation("Completed: Setting status of inactive users...");
        }
    }

}
