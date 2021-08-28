using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Stock
{
    public static class TransferDetailEfCoreQueryableExtensions
    {
        public static IQueryable<TransferDetail> IncludeDetails(this IQueryable<TransferDetail> queryable, bool include = true)
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