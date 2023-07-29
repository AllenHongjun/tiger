using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.TaskManagement
{
    public interface IBackgroundJobInfoRepository:IRepository<BackgroundJobInfo,string>
    {
        Task<bool> CheckNameAsync(
            string group,
            string name,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取作业
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<JobInfo> FindJobAsync(
            string id,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取过期任务列表
        /// </summary>
        /// <param name="maxResultCount"></param>
        /// <param name="jobExpiratime"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<BackgroundJobInfo>> GetExpiredJobsAsync(
            int maxResultCount,
            TimeSpan jobExpiratime,
            CancellationToken cancellationToken= default);

        /// <summary>
        /// 获取所有周期性任务
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<BackgroundJobInfo>> GetAllPeriodTaskAsync(
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取等待入队的任务列表
        /// </summary>
        /// <param name="maxResultCount"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<List<BackgroundJobInfo>> GetWaitingListAsync(
            int maxResultCount,
            CancellationToken cancellation =default);

        //Task<int> GetCountAsync(
        //    B)
    }
}
