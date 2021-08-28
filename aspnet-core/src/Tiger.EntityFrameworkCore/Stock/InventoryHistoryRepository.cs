using System;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Stock
{
    public class InventoryHistoryRepository : EfCoreRepository<TigerDbContext, InventoryHistory, Guid>, IInventoryHistoryRepository
    {
        public InventoryHistoryRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}