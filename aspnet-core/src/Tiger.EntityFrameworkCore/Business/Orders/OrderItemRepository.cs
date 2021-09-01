using System;
using System.Collections.Generic;
using System.Text;
using Tiger.EntityFrameworkCore;
using Tiger.Orders;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Orders
{
    public class OrderItemRepository : EfCoreRepository<TigerDbContext, OrderItem, Guid>, IOrderItemRepository
    {
        public OrderItemRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
