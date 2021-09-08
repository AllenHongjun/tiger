using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Business.Orders
{
    public static class OrderReturnDetailEfCoreQueryableExtensions
    {
        public static IQueryable<OrderReturnDetail> IncludeDetails(this IQueryable<OrderReturnDetail> queryable, bool include = true)
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