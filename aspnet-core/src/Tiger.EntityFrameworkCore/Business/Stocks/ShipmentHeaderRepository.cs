using System;
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
    }
}