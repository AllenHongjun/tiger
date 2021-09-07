using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Stock
{
    public class TransferHeaderRepository : EfCoreRepository<TigerDbContext, TransferHeader, Guid>, ITransferHeaderRepository
    {
        public TransferHeaderRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<TransferHeader> GetAsync(Guid id)
        {
            return await DbSet.AsNoTracking()
                .Include(x => x.TransferDetails)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}