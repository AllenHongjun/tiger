using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Stock
{
    public static class TransferHeaderEfCoreQueryableExtensions
    {
        public static IQueryable<TransferHeader> IncludeDetails(this IQueryable<TransferHeader> queryable, bool include = true)
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