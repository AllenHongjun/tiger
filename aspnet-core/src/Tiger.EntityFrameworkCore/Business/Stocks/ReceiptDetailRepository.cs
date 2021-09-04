using System;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Stocks
{
    public class ReceiptDetailRepository : EfCoreRepository<TigerDbContext, ReceiptDetail, Guid>, IReceiptDetailRepository
    {
        public ReceiptDetailRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}