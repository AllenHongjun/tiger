using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Orders
{
    public class OrderReturnHeaderRepository : EfCoreRepository<TigerDbContext, OrderReturnHeader, Guid>, IOrderReturnHeaderRepository
    {
        public OrderReturnHeaderRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<OrderReturnHeader> GetAsync(Guid id)
        {
            return await DbSet.AsNoTracking()
                .Include(x => x.OrderReturnDetails)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}