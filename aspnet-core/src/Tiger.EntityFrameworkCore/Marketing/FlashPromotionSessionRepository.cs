using System;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Marketing
{
    public class FlashPromotionSessionRepository : EfCoreRepository<TigerDbContext, FlashPromotionSession, Guid>, IFlashPromotionSessionRepository
    {
        public FlashPromotionSessionRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}