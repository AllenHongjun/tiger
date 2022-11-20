using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.BackgroundWorkers;

namespace Tiger.Infrastructure.BackgroundWorker
{

    /// <summary>
    /// 后台定时作业（后台工作者 demo）
    /// BackgroundWorkerBase 是创建后台工作者的简单方法.
    /// </summary>
    public class MyWorker : BackgroundWorkerBase
    {

        /// <summary>
        /// startAsync 开始你的工作者(在应用程序启动时),
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task StartAsync(CancellationToken cancellationToken = default)
        {
            //...

            return Task.CompletedTask;
        }

        /// <summary>
        /// topAsync 停止它(在应用程序关闭时).
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task StopAsync(CancellationToken cancellationToken = default)
        {
            //...

            return Task.CompletedTask;
        }
    }

}
