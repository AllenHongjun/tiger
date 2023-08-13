﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;
using Tiger.Infrastructure.BackgroundTasks.Abstractions.Enum;
using Volo.Abp.DependencyInjection;

namespace Tiger.Infrastructure.BackgroundTasks.Internal;

[Dependency(TryRegister = true)]
internal class InMemoryJobStore : IJobStore, ISingletonDependency
{
    private readonly List<JobInfo> _memoryJobStore;

    public InMemoryJobStore()
    {
        _memoryJobStore = new List<JobInfo>();
    }

    public Task<List<JobInfo>> GetAllPeriodTasksAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var status = new JobStatus[] { JobStatus.Running, JobStatus.FailedRetry };

        var jobs = _memoryJobStore
            .Where(x => !x.IsAbandoned)
            .Where(x => x.JobType == JobType.Period && status.Contains(x.Status))
            .Where(x => x.MaxCount == 0 || x.TriggerCount < x.MaxCount || x.MaxTryCount == 0 || x.TryCount < x.MaxTryCount)
            .OrderByDescending(x => x.Priority)
            .ToList();

        return Task.FromResult(jobs);
    }

    public Task<List<JobInfo>> GetWaitingListAsync(int maxResultCount, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var now = DateTime.Now;
        var status = new JobStatus[] { JobStatus.Running, JobStatus.FailedRetry };

        var jobs = _memoryJobStore
            .Where(x => !x.IsAbandoned)
            .Where(x => x.JobType != JobType.Period && status.Contains(x.Status))
            .Where(x => x.MaxCount == 0 || x.TriggerCount < x.MaxCount || x.MaxTryCount == 0 || x.TryCount < x.MaxTryCount)
            .OrderByDescending(x => x.Priority)
            .ThenBy(x => x.TryCount)
            .ThenBy(x => x.NextRunTime)
            .Take(maxResultCount)
            .ToList();

        return Task.FromResult(jobs);
    }

    public Task<JobInfo> FindAsync(
        string jobId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var job = _memoryJobStore.FirstOrDefault(x => x.Id.Equals(jobId));
        return Task.FromResult(job);
    }

    public Task StoreAsync(
        JobInfo jobInfo,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var job = _memoryJobStore.FirstOrDefault(x => x.Id.Equals(jobInfo.Id));
        if (job != null)
        {
            job.NextRunTime = jobInfo.NextRunTime;
            job.LastRunTime = jobInfo.LastRunTime;
            job.Status = jobInfo.Status;
            job.TriggerCount = jobInfo.TriggerCount;
            job.TryCount = jobInfo.TryCount;
            job.IsAbandoned = jobInfo.IsAbandoned;
        }
        else
        {
            _memoryJobStore.Add(jobInfo);
        }
        return Task.CompletedTask;
    }

    public async Task RemoveAsync(
        string jobId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var job = await FindAsync(jobId, cancellationToken);
        if (job != null)
        {
            _memoryJobStore.Remove(job);
        }
    }

    public Task StoreLogAsync(JobEventData eventData)
    {
        eventData.CancellationToken.ThrowIfCancellationRequested();

        return Task.CompletedTask;
    }

    public Task CleanupAsync(int maxResultCount, TimeSpan jobExpiratime, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var expiratime = DateTime.Now - jobExpiratime;

        var expriaJobs = _memoryJobStore
            .Where(x => x.Status == JobStatus.Completed &&
                expiratime.CompareTo(x.LastRunTime ?? x.EndTime ?? x.CreationTime) <= 0)
            .Take(maxResultCount);

        //Todo:优化使用removeAll方法
        foreach (var item in expriaJobs)
        {
            _memoryJobStore.Remove(item);
        }
        return Task.CompletedTask;
    }
}
