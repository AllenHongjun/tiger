using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Infrastructure.BackgroundTasks
{
    /// <summary>
    /// 作业锁定提供者
    /// </summary>
    public interface IJobLockProvider
    {
        Task<bool> TryLockAsync(
        string jobKey,
        int lockSeconds);

        Task<bool> TryReleaseAsync(
            string jobKey);
    }
}
