using System;
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
    }
}