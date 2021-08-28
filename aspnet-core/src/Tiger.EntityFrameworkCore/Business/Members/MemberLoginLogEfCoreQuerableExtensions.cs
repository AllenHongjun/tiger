using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Business.Members
{
    public static class MemberLoginLogEfCoreQueryableExtensions
    {
        public static IQueryable<MemberLoginLog> IncludeDetails(this IQueryable<MemberLoginLog> queryable, bool include = true)
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