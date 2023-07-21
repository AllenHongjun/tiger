using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.System.Platform.Versions
{
    public class VersionRepository : 
        EfCoreRepository<TigerDbContext, AppVersion, Guid>, IVersionRepository, ITransientDependency
    {
        public VersionRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task<bool> ExistsAsync(PlatformType platformType, string version, CancellationToken cancellationToken = default)
        {
            return DbSet
                .AnyAsync(x => (platformType | x.PlatformType) == platformType && x.Version.Equals(version), GetCancellationToken(cancellationToken));
        }

        public async Task<AppVersion> GetByVersionAsync(PlatformType platformType, string version, CancellationToken cancellationToken = default)
        {   
            return await DbSet
                .Include(x => x.Files)
                .Where(x => x.PlatformType == (platformType | x.PlatformType) && x.Version.Equals(version))
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(PlatformType platformType, string filter = "", CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(x => (platformType | x.PlatformType) == x.PlatformType)
                .WhereIf(!filter.IsNullOrWhiteSpace(), x => x.Version.Contains(filter) || x.Title.Contains(filter))
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<AppVersion>> GetPagedListAsync(
            PlatformType platformType, string filter = "", string sorting = nameof(AppVersion.CreationTime), 
            bool includeDetails = true, int skipCount = 1, int maxResultCount = 10, 
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .IncludeIf(includeDetails, x => x.Files)
                .Where(x => (platformType | x.PlatformType) == x.PlatformType)
                .WhereIf(!filter.IsNullOrWhiteSpace(), x => x.Version.Contains(filter) || x.Title.Contains(filter))
                .OrderBy($"{nameof(AppVersion.CreationTime)} DESC")
                .ThenBy(sorting ?? nameof(AppVersion.Version))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
