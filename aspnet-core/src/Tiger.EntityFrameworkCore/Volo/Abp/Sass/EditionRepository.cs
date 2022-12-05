using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Tiger.Volo.Abp.Sass.Editions;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.TenantManagement;

namespace Tiger.Volo.Abp.Sass
{   
    /// <summary>
    /// 版本仓储实现
    /// </summary>
    public class EditionRepository : EfCoreRepository<TigerDbContext, Edition, Guid>, IEditionRepository
    {
        public EditionRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<bool> CheckUsedByTenantAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {   
            // 需要扩充 tenant 的字段

            //return await DbContext.Set<Tenant>()
            //    .AnyAsync(x => x.E)
            throw new NotImplementedException();
        }

        public Task<Edition> FindByDisplayNameAsync(string displayName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Edition> FindByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<long> GetCountAsync(string filter = null, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<Edition>()
                    .WhereIf(!filter.IsNullOrEmpty(), x => x.DisplayName.Contains(filter))
                    .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Edition>> GetListAsync(string sorting = null, int maxResultCount = 50, int skipCount = 0, string filter = null, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<Edition>()
                    .WhereIf(!filter.IsNullOrEmpty(), x => x.DisplayName.Contains(filter))
                    .OrderBy(sorting.IsNullOrEmpty() ? nameof(Edition.DisplayName) : sorting)
                    .PageBy(skipCount, maxResultCount)
                    .ToListAsync(GetCancellationToken(cancellationToken));
                
        }
    }
}
