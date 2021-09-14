using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Business.Basic
{
    public static class ProductAttributeValueEfCoreQueryableExtensions
    {
        public static IQueryable<ProductAttributeValue> IncludeDetails(this IQueryable<ProductAttributeValue> queryable, bool include = true)
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