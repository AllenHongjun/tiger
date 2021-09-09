using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Business.Purchases
{
    public static class PurchaseHeaderEfCoreQueryableExtensions
    {
        public static IQueryable<PurchaseHeader> IncludeDetails(this IQueryable<PurchaseHeader> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                 .Include(x => x.PurchaseDetails) // TODO: AbpHelper generated
                ;
        }
    }
}