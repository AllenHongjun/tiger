using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Marketing
{
    public static class FlashPromotionSessionEfCoreQueryableExtensions
    {
        public static IQueryable<FlashPromotionSession> IncludeDetails(this IQueryable<FlashPromotionSession> queryable, bool include = true)
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