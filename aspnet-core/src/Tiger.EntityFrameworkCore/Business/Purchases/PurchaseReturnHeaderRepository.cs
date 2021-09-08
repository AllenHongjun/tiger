using System;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Purchases
{
    public class PurchaseReturnHeaderRepository : EfCoreRepository<TigerDbContext, PurchaseReturnHeader, Guid>, IPurchaseReturnHeaderRepository
    {
        public PurchaseReturnHeaderRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}