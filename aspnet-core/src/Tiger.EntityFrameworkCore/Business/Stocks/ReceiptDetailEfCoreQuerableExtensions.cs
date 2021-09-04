using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Business.Stocks
{
    public static class ReceiptDetailEfCoreQueryableExtensions
    {
        public static IQueryable<ReceiptDetail> IncludeDetails(this IQueryable<ReceiptDetail> queryable, bool include = true)
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