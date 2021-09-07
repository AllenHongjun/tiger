using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Stock
{
    public class CheckHeaderRepository : EfCoreRepository<TigerDbContext, CheckHeader, Guid>, ICheckHeaderRepository
    {
        public CheckHeaderRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<CheckHeader> GetAsync(Guid id)
        {
            return await DbSet.AsNoTracking()
                .Include(x => x.CheckDetails)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}