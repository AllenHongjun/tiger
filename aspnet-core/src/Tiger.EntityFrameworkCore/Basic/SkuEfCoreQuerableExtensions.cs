using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Basic
{
    public static class SkuEfCoreQueryableExtensions
    {
        public static IQueryable<Sku> IncludeDetails(this IQueryable<Sku> queryable, bool include = true)
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