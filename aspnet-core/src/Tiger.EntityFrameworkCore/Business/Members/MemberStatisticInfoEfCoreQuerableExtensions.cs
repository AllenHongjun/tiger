using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Business.Members
{
    public static class MemberStatisticInfoEfCoreQueryableExtensions
    {
        public static IQueryable<MemberStatisticInfo> IncludeDetails(this IQueryable<MemberStatisticInfo> queryable, bool include = true)
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