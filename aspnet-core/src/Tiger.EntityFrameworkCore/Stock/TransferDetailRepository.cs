using System;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Stock
{
    public class TransferDetailRepository : EfCoreRepository<TigerDbContext, TransferDetail, Guid>, ITransferDetailRepository
    {
        public TransferDetailRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}