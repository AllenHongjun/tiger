using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Stock
{
    public static class CheckHeaderEfCoreQueryableExtensions
    {
        public static IQueryable<CheckHeader> IncludeDetails(this IQueryable<CheckHeader> queryable, bool include = true)
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