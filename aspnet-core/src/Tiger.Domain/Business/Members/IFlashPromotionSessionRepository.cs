using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Marketing
{
    public interface IFlashPromotionSessionRepository : IRepository<FlashPromotionSession, Guid>
    {
    }
}