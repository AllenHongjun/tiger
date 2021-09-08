using System;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Purchases
{
    public class PurchaseReturnDetailRepository : EfCoreRepository<TigerDbContext, PurchaseReturnDetail, Guid>, IPurchaseReturnDetailRepository
    {
        public PurchaseReturnDetailRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}