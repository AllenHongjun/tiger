using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
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

        public override Task<ReceiptHeader> GetAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return base.GetAsync(id, includeDetails, cancellationToken);
        }

        public override Task<ReceiptHeader> InsertAsync(ReceiptHeader entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return base.InsertAsync(entity, autoSave, cancellationToken);
        }

        public override Task<ReceiptHeader> UpdateAsync(ReceiptHeader entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return base.UpdateAsync(entity, autoSave, cancellationToken);
        }

        public override Task DeleteAsync(ReceiptHeader entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return base.DeleteAsync(entity, autoSave, cancellationToken);
        }
    }
}