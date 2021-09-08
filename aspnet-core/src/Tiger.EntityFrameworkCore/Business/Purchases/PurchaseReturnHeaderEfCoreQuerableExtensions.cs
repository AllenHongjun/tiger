using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Business.Purchases
{
    public static class PurchaseReturnHeaderEfCoreQueryableExtensions
    {
        public static IQueryable<PurchaseReturnHeader> IncludeDetails(this IQueryable<PurchaseReturnHeader> queryable, bool include = true)
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