﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.TaskManagement;

public class EfCoreBackgroundJobLogRepository :
    EfCoreRepository<TigerDbContext, BackgroundJobLog, long>,
    IBackgroundJobLogRepository
{
    public EfCoreBackgroundJobLogRepository(
        IDbContextProvider<TigerDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async virtual Task<int> GetCountAsync(
        BackgroundJobLogFilter filter,
        string jobId = null,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .WhereIf(!jobId.IsNullOrWhiteSpace(), x => x.JobId.Equals(jobId))
            .WhereIf(!filter.Type.IsNullOrWhiteSpace(), x => x.JobType.Contains(filter.Type))
            .WhereIf(!filter.Group.IsNullOrWhiteSpace(), x => x.JobGroup.Equals(filter.Group))
            .WhereIf(!filter.Name.IsNullOrWhiteSpace(), x => x.JobName.Equals(filter.Name))
            .WhereIf(!filter.Filter.IsNullOrWhiteSpace(), x => x.JobName.Contains(filter.Filter) ||
                x.JobGroup.Contains(filter.Filter) || x.JobType.Contains(filter.Filter) || x.Message.Contains(filter.Filter))
            .WhereIf(filter.HasExceptions.HasValue, x => !string.IsNullOrWhiteSpace(x.Exception))
            .WhereIf(filter.BeginRunTime.HasValue, x => x.RunTime.CompareTo(filter.BeginRunTime.Value) >= 0)
            .WhereIf(filter.EndRunTime.HasValue, x => x.RunTime.CompareTo(filter.EndRunTime.Value) <= 0)
            .CountAsync(GetCancellationToken(cancellationToken));
    }

    public async virtual Task<List<BackgroundJobLog>> GetListAsync(
        BackgroundJobLogFilter filter,
        string jobId = null,
        string sorting = nameof(BackgroundJobLog.RunTime),
        int maxResultCount = 10,
        int skipCount = 0,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .WhereIf(!jobId.IsNullOrWhiteSpace(), x => x.JobId.Equals(jobId))
            .WhereIf(!filter.Type.IsNullOrWhiteSpace(), x => x.JobType.Contains(filter.Type))
            .WhereIf(!filter.Group.IsNullOrWhiteSpace(), x => x.JobGroup.Equals(filter.Group))
            .WhereIf(!filter.Name.IsNullOrWhiteSpace(), x => x.JobName.Equals(filter.Name))
            .WhereIf(!filter.Filter.IsNullOrWhiteSpace(), x => x.JobName.Contains(filter.Filter) ||
                x.JobGroup.Contains(filter.Filter) || x.JobType.Contains(filter.Filter) || x.Message.Contains(filter.Filter))
            .WhereIf(filter.HasExceptions.HasValue, x => !string.IsNullOrWhiteSpace(x.Exception))
            .WhereIf(filter.BeginRunTime.HasValue, x => x.RunTime.CompareTo(filter.BeginRunTime.Value) >= 0)
            .WhereIf(filter.EndRunTime.HasValue, x => x.RunTime.CompareTo(filter.EndRunTime.Value) <= 0)
            .OrderBy(sorting ?? $"{nameof(BackgroundJobLog.RunTime)} DESC")
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
}
