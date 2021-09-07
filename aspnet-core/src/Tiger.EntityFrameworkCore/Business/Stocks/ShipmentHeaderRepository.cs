using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Stocks
{
    public class ShipmentHeaderRepository : EfCoreRepository<TigerDbContext, ShipmentHeader, Guid>, IShipmentHeaderRepository
    {
        public ShipmentHeaderRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        //public override Task<ShipmentHeader> GetAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = default)
        //{
        //    return base.GetAsync(id, includeDetails, cancellationToken);
        //}

        public async Task<ShipmentHeader> GetAsync(Guid id)
        {
            return await DbSet.AsNoTracking()
                .Include(x => x.ShipmentDetails)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}