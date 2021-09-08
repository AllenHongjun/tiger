using System;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Purchases
{
    public class PurchaseHeaderRepository : EfCoreRepository<TigerDbContext, PurchaseHeader, Guid>, IPurchaseHeaderRepository
    {
        public PurchaseHeaderRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}