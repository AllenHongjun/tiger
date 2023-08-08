using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Tiger.Infrastructure.BackgroundTasks
{
    public class DefaultJobLockProvider : IJobLockProvider, ISingletonDependency
    {
        private readonly ConcurrentDictionary<string, JobLock> _localSyncObjects = new();

        public virtual Task<bool> TryLockAsync(string jobKey, int lockSeconds)
        {
            Check.NotNullOrWhiteSpace(jobKey, nameof(jobKey));
            if (_localSyncObjects.TryGetValue(jobKey, out var jobLock))
            {
                if (jobLock.ExpirationTime > DateTime.UtcNow)
                {
                    return Task.FromResult(false);
                }
                jobLock.ExpirationTime = DateTime.UtcNow.AddSeconds(lockSeconds);
                return Task.FromResult(true);
            }

            jobLock = new JobLock
            {
                ExpirationTime = DateTime.UtcNow.AddSeconds(lockSeconds),
                Semaphore = new SemaphoreSlim(1, 1)
            };

            return Task.FromResult(_localSyncObjects.TryAdd(jobKey, jobLock));
        }

        public Task<bool> TryReleaseAsync(string jobKey)
        {
            Check.NotNullOrWhiteSpace(jobKey, nameof(jobKey));

            if (_localSyncObjects.TryGetValue(jobKey, out var jobLock))
            {
                jobLock.Semaphore.Dispose();

                return Task.FromResult(_localSyncObjects.TryRemove(jobKey, out _));
            }

            return Task.FromResult(false);
        }

        private class JobLock
        {
            public DateTime ExpirationTime { get; set; }
            public SemaphoreSlim Semaphore { get; set; }
        }
    }
}
