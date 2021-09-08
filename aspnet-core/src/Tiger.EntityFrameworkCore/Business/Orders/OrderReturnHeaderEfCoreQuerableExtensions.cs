using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Business.Orders
{
    public static class OrderReturnHeaderEfCoreQueryableExtensions
    {
        public static IQueryable<OrderReturnHeader> IncludeDetails(this IQueryable<OrderReturnHeader> queryable, bool include = true)
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