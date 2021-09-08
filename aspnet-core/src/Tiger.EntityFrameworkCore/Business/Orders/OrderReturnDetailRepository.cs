using System;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Orders
{
    public class OrderReturnDetailRepository : EfCoreRepository<TigerDbContext, OrderReturnDetail, Guid>, IOrderReturnDetailRepository
    {
        public OrderReturnDetailRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}