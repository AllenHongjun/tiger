using System;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Stocks
{
    public class ShipmentDetailRepository : EfCoreRepository<TigerDbContext, ShipmentDetail, Guid>, IShipmentDetailRepository
    {
        public ShipmentDetailRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task<ShipmentHeader> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}