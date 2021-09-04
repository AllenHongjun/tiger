using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Stock
{
    public class ReceiptHeaderRepository : EfCoreRepository<TigerDbContext, ReceiptHeader, Guid>, IReceiptHeaderRepository
    {
        public ReceiptHeaderRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public  async Task<ReceiptHeader> GetAsync(Guid id)
        {
            return await DbSet.AsNoTracking()
                .Include(x => x.ReceiptDetails)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}