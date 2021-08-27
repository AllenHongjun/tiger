using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Basic
{
    public static class SupplyEfCoreQueryableExtensions
    {
        public static IQueryable<Supply> IncludeDetails(this IQueryable<Supply> queryable, bool include = true)
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