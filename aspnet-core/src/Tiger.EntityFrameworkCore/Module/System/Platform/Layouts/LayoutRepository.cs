using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.System.Platform.Layouts
{
    public class LayoutRepository : EfCoreRepository<TigerDbContext, Layout, Guid>, ILayoutRepository
    {
        public LayoutRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async virtual Task<Layout> FindByNameAsync(
            string name,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .IncludeDetails(includeDetails)
                .Where(x => x.Name == name)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        public async virtual Task<int> GetCountAsync(
            string framework = "",
            string filter = "",
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .WhereIf(!framework.IsNullOrWhiteSpace(), x => x.Framework.Equals(framework))
                .WhereIf(!filter.IsNullOrWhiteSpace(), x =>
                        x.Name.Contains(filter) || x.DisplayName.Contains(filter) ||
                        x.Description.Contains(filter) || x.Redirect.Contains(filter))
                .CountAsync(GetCancellationToken(cancellationToken));
        }

        public async virtual Task<List<Layout>> GetPagedListAsync(
            string framework = "",
            string filter = "",
            string sorting = nameof(Layout.Name),
            bool reverse = false,
            bool includeDetails = false,
            int skipCount = 0,
            int maxResultCount = 10,
            CancellationToken cancellationToken = default)
        {
            sorting ??= nameof(Layout.Name);
            sorting = reverse ? sorting + " DESC" : sorting;

            return await DbSet
                .IncludeDetails(includeDetails)
                .WhereIf(!framework.IsNullOrWhiteSpace(), x => x.Framework.Equals(framework))
                .WhereIf(!filter.IsNullOrWhiteSpace(), x =>
                        x.Name.Contains(filter) || x.DisplayName.Contains(filter) ||
                        x.Description.Contains(filter) || x.Redirect.Contains(filter))
                .OrderBy(sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        
    }
}
