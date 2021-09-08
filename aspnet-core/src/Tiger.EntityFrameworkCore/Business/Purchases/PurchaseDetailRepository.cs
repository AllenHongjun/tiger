using System;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Purchases
{
    public class PurchaseDetailRepository : EfCoreRepository<TigerDbContext, PurchaseDetail, Guid>, IPurchaseDetailRepository
    {
        public PurchaseDetailRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}