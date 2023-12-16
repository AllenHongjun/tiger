﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;
using Tiger.Infrastructure.BackgroundTasks.Abstractions.Enum;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Timing;

namespace Tiger.Module.TaskManagement;

public class EfCoreBackgroundJobInfoRepository :
    EfCoreRepository<TigerDbContext, BackgroundJobInfo, string>,
    IBackgroundJobInfoRepository
{
    protected IClock Clock { get; }

    public EfCoreBackgroundJobInfoRepository(
        IClock clock,
        IDbContextProvider<TigerDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
        Clock = clock;
    }

    public async Task<BackgroundJobInfo> FindAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = default(CancellationToken))
    {
        return null;
    }

    public async virtual Task<bool> CheckNameAsync(
        string group,
        string name,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AnyAsync(x => x.Group.Equals(group) && x.Name.Equals(name),
                GetCancellationToken(cancellationToken));
    }

    public async virtual Task<JobInfo> FindJobAsync(
        string id,
        bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(x => x.Id.Equals(id))
            .Select(x => new JobInfo
            {
                Id = x.Id,
                TenantId = x.TenantId,
                Name = x.Name,
                NextRunTime = x.NextRunTime,
                Args = x.Args,
                IsAbandoned = x.IsAbandoned,
                BeginTime = x.BeginTime,
                EndTime = x.EndTime,
                CreationTime = x.CreationTime,
                Cron = x.Cron,
                MaxCount = x.MaxCount,
                MaxTryCount = x.MaxTryCount,
                Description = x.Description,
                Group = x.Group,
                Interval = x.Interval,
                JobType = x.JobType,
                Status = x.Status,
                Priority = x.Priority,
                Source = x.Source,
                LastRunTime = x.LastRunTime,
                LockTimeOut = x.LockTimeOut,
                Result = x.Result,
                TriggerCount = x.TriggerCount,
                TryCount = x.TryCount,
                Type = x.Type,
                NodeName = x.NodeName,
            })
            .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
    }

    public async virtual Task<List<BackgroundJobInfo>> GetExpiredJobsAsync(
        int maxResultCount,
        TimeSpan jobExpiratime,
        CancellationToken cancellationToken = default)
    {
        var expiratime = Clock.Now - jobExpiratime;

        return await DbSet
            .Where(x => x.Status == JobStatus.Completed &&
                DateTime.Compare(x.LastRunTime.Value, expiratime) <= 0)
            .OrderBy(x => x.CreationTime)
            .Take(maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async virtual Task<List<BackgroundJobInfo>> GetAllPeriodTasksAsync(
        CancellationToken cancellationToken = default)
    {
        var status = new JobStatus[] { JobStatus.Running, JobStatus.FailedRetry };

        return await DbSet
            .Where(x => x.IsEnabled && !x.IsAbandoned)
            .Where(x => x.JobType == JobType.Period && status.Contains(x.Status))
            .Where(x => x.MaxCount == 0 || x.TriggerCount < x.MaxCount || x.MaxTryCount == 0 || x.TryCount < x.MaxTryCount)
            .OrderByDescending(x => x.Priority)
            .AsNoTracking()
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async virtual Task<int> GetCountAsync(BackgroundJobInfoFilter filter, CancellationToken cancellationToken = default)
    {
        return await ApplyFilter(DbSet, filter)
            .CountAsync(GetCancellationToken(cancellationToken));
    }

    public async virtual Task<List<BackgroundJobInfo>> GetListAsync(BackgroundJobInfoFilter filter, string sorting = "Name", int maxResultCount = 10, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        return await ApplyFilter(DbSet, filter)
            .OrderBy(sorting ?? $"{nameof(BackgroundJobInfo.CreationTime)} DESC")
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async virtual Task<List<BackgroundJobInfo>> GetWaitingListAsync(
        int maxResultCount,
        CancellationToken cancellationToken = default)
    {
        var now = Clock.Now;
        var status = new JobStatus[] { JobStatus.Running, JobStatus.FailedRetry };

        return await DbSet
            .Where(x => x.IsEnabled && !x.IsAbandoned)
            .Where(x => x.JobType != JobType.Period && status.Contains(x.Status))
            .Where(x => x.MaxCount == 0 || x.TriggerCount < x.MaxCount || x.MaxTryCount == 0 || x.TryCount < x.MaxTryCount)
            .OrderByDescending(x => x.Priority)
            .ThenBy(x => x.TryCount)
            .ThenBy(x => x.NextRunTime)
            .Take(maxResultCount)
            .AsNoTracking()
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<BackgroundJobInfo> ApplyFilter(
        IQueryable<BackgroundJobInfo> queryable,
        BackgroundJobInfoFilter filter)
    {
        return queryable
            .WhereIf(!filter.Type.IsNullOrWhiteSpace(), x => x.Type.Contains(filter.Type))
            .WhereIf(!filter.Group.IsNullOrWhiteSpace(), x => x.Group.Equals(filter.Group))
            .WhereIf(!filter.Name.IsNullOrWhiteSpace(), x => x.Name.Equals(filter.Name))
            .WhereIf(!filter.Filter.IsNullOrWhiteSpace(), x => x.Name.Contains(filter.Filter) ||
                x.Group.Contains(filter.Filter) || x.Type.Contains(filter.Filter) || x.Description.Contains(filter.Filter))
            .WhereIf(filter.JobType.HasValue, x => x.JobType == filter.JobType)
            .WhereIf(filter.Status.HasValue, x => x.Status == filter.Status.Value)
            .WhereIf(filter.Priority.HasValue, x => x.Priority == filter.Priority.Value)
            .WhereIf(filter.Source.HasValue, x => x.Source == filter.Source.Value)
            .WhereIf(filter.IsAbandoned.HasValue, x => x.IsAbandoned == filter.IsAbandoned.Value)
            .WhereIf(filter.BeginLastRunTime.HasValue, x => filter.BeginLastRunTime.Value.CompareTo(x.LastRunTime) <= 0)
            .WhereIf(filter.EndLastRunTime.HasValue, x => filter.EndLastRunTime.Value.CompareTo(x.LastRunTime) >= 0)
            .WhereIf(filter.BeginTime.HasValue, x => x.BeginTime.CompareTo(x.BeginTime) >= 0)
            .WhereIf(filter.EndTime.HasValue, x => filter.EndTime.Value.CompareTo(x.EndTime) >= 0)
            .WhereIf(filter.BeginCreationTime.HasValue, x => x.CreationTime.CompareTo(filter.BeginCreationTime.Value) >= 0)
            .WhereIf(filter.EndCreationTime.HasValue, x => x.CreationTime.CompareTo(filter.EndCreationTime.Value) <= 0);
    }
}
