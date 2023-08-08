﻿using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.DistributedLocking;

namespace Tiger.Infrastructure.BackgroundTasks.DistributedLocking;

[Dependency(ReplaceServices = true)]
public class JobDistributedLockingProvider : IJobLockProvider, ISingletonDependency
{
    protected IMemoryCache LockCache { get; }
    protected IAbpDistributedLock DistributedLock { get; }

    public JobDistributedLockingProvider(
        IMemoryCache lockCache,
        IAbpDistributedLock distributedLock)
    {
        LockCache = lockCache;
        DistributedLock = distributedLock;
    }

    public async virtual Task<bool> TryLockAsync(string jobKey, int lockSeconds)
    {
        var handle = await DistributedLock.TryAcquireAsync(jobKey);
        if (handle != null)
        {
            await LockCache.GetOrCreateAsync(jobKey, (entry) =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(lockSeconds));
                entry.RegisterPostEvictionCallback(async (key, value, reason, state) =>
                {
                    if (reason == EvictionReason.Expired && value is IAbpDistributedLockHandle handleValue)
                    {
                        await handleValue.DisposeAsync();
                    }
                });
                entry.SetValue(handle);

                return Task.FromResult(handle);
            });

            return true;
        }
        return false;
    }

    public async virtual Task<bool> TryReleaseAsync(string jobKey)
    {
        if (LockCache.TryGetValue<IAbpDistributedLockHandle>(jobKey, out var handle))
        {
            await handle.DisposeAsync();

            LockCache.Remove(jobKey);

            return true;
        }
        return false;
    }
}
