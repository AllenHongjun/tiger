using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Purchases
{
    public class PurchaseReturnHeaderRepository : EfCoreRepository<TigerDbContext, PurchaseReturnHeader, Guid>, IPurchaseReturnHeaderRepository
    {
        public PurchaseReturnHeaderRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<PurchaseReturnHeader> GetAsync(Guid id)
        {
            return await DbSet.AsNoTracking()
                .Include(x => x.PurchaseReturnDetails)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}