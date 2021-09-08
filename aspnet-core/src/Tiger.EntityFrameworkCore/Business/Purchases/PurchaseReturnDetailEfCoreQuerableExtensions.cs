using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Business.Purchases
{
    public static class PurchaseReturnDetailEfCoreQueryableExtensions
    {
        public static IQueryable<PurchaseReturnDetail> IncludeDetails(this IQueryable<PurchaseReturnDetail> queryable, bool include = true)
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