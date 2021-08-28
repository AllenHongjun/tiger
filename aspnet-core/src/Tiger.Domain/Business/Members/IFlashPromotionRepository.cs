using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Marketing
{
    public interface IFlashPromotionRepository : IRepository<FlashPromotion, Guid>
    {
    }
}