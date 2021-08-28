using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Stock
{
    public static class CheckDetailEfCoreQueryableExtensions
    {
        public static IQueryable<CheckDetail> IncludeDetails(this IQueryable<CheckDetail> queryable, bool include = true)
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