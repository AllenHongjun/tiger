using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Purchases
{
    public class PurchaseHeaderRepository : EfCoreRepository<TigerDbContext, PurchaseHeader, Guid>, IPurchaseHeaderRepository
    {
        public PurchaseHeaderRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<PurchaseHeader> GetAsync(Guid id)
        {
            return await DbSet.AsNoTracking()
                .Include(x => x.PurchaseDetails)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}