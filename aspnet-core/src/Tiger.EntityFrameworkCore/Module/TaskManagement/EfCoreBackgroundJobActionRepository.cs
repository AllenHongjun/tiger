using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.TaskManagement;
public class EfCoreBackgroundJobActionRepository :
    EfCoreRepository<TigerDbContext, BackgroundJobAction, Guid>,
    IBackgroundJobActionRepository
{
    public EfCoreBackgroundJobActionRepository(
        IDbContextProvider<TigerDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async virtual Task<List<BackgroundJobAction>> GetListAsync(
        string jobId,
        bool? isEnabled = null,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(x => x.JobId.Equals(jobId))
            .WhereIf(isEnabled.HasValue, x => x.IsEnabled == isEnabled.Value)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
}
