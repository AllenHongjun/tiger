using System;
using System.Collections.Generic;
using System.Text;
using Tiger.EntityFrameworkCore;
using Tiger.Orders;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Orders
{
    public class CartItemRepository : EfCoreRepository<TigerDbContext, CartItem, Guid>, ICartItemRepository
    {
        public CartItemRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
