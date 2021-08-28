using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Business.Stocks
{
    public static class ShipmentHeaderEfCoreQueryableExtensions
    {
        public static IQueryable<ShipmentHeader> IncludeDetails(this IQueryable<ShipmentHeader> queryable, bool include = true)
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