using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Stock
{
    public static class ReceiptHeaderEfCoreQueryableExtensions
    {
        public static IQueryable<ReceiptHeader> IncludeDetails(this IQueryable<ReceiptHeader> queryable, bool include = true)
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