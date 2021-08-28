using System;
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
    }
}