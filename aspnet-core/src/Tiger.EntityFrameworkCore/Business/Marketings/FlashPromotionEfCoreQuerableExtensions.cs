using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Marketing
{
    public static class FlashPromotionEfCoreQueryableExtensions
    {
        public static IQueryable<FlashPromotion> IncludeDetails(this IQueryable<FlashPromotion> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                // .Include(x => x.xxx) // TODO: AbpHelper generated
                ;
        }
    }
}