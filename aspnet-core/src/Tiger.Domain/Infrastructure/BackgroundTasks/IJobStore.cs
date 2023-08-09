using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;

namespace Tiger.Infrastructure.BackgroundTasks
{
    public interface IJobStore
    {
        /// <summary>
        /// 查询等待的作业
        /// </summary>
        /// <param name="maxResultCount"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<JobInfo>> GetWaitingListAsync(
        int maxResultCount,
        CancellationToken cancellationToken = default);

        Task<List<JobInfo>> GetAllPeriodTasksAsync(
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据作业Id查询
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<JobInfo> FindAsync(
            string jobId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 保存作业
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task StoreAsync(
            JobInfo jobInfo,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        Task StoreLogAsync(JobEventData eventData);

        /// <summary>
        /// 根据作业Id移除作业
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync(
            string jobId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 清除所有作业
        /// </summary>
        /// <param name="maxResultCount"></param>
        /// <param name="jobExpiratime"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CleanupAsync(
            int maxResultCount,
            TimeSpan jobExpiratime,
            CancellationToken cancellationToken = default);
    }
}
