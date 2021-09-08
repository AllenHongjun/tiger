using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Business.Purchases
{
    public static class PurchaseDetailEfCoreQueryableExtensions
    {
        public static IQueryable<PurchaseDetail> IncludeDetails(this IQueryable<PurchaseDetail> queryable, bool include = true)
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