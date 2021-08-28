using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Stock
{
    public static class InventoryHistoryEfCoreQueryableExtensions
    {
        public static IQueryable<InventoryHistory> IncludeDetails(this IQueryable<InventoryHistory> queryable, bool include = true)
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