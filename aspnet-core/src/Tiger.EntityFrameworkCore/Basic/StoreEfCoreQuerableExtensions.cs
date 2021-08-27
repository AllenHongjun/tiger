using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Basic
{
    public static class StoreEfCoreQueryableExtensions
    {
        public static IQueryable<Store> IncludeDetails(this IQueryable<Store> queryable, bool include = true)
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