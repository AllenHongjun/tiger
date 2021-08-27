using System;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Marketing
{
    public class FlashPromotionRepository : EfCoreRepository<TigerDbContext, FlashPromotion, Guid>, IFlashPromotionRepository
    {
        public FlashPromotionRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}