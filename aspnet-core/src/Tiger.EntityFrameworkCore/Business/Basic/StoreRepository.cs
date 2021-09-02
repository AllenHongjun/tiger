using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Basic
{
    public class StoreRepository : EfCoreRepository<TigerDbContext, Store, Guid>, IStoreRepository
    {
        public StoreRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Store>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            return await DbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    product => product.Name.Contains(filter)
                 )
                .OrderBy(sorting => sorting.LastModificationTime)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task<List<Store>> GetAllStore()
        {
            return  await DbSet.WhereIf(true, x => x.IsDeleted == false)
                .OrderByDescending(sorting => sorting.Id)
                .ToListAsync(); 
        }
    }
}