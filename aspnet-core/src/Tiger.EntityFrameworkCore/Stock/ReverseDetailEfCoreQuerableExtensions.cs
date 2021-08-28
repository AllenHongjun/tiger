using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Stock
{
    public static class ReverseDetailEfCoreQueryableExtensions
    {
        public static IQueryable<ReverseDetail> IncludeDetails(this IQueryable<ReverseDetail> queryable, bool include = true)
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