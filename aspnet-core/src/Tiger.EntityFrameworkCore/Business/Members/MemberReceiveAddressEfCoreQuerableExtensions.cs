using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Business.Members
{
    public static class MemberReceiveAddressEfCoreQueryableExtensions
    {
        public static IQueryable<MemberReceiveAddress> IncludeDetails(this IQueryable<MemberReceiveAddress> queryable, bool include = true)
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