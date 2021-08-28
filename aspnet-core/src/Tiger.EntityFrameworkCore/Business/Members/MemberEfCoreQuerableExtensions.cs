using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Business.Members
{
    public static class MemberEfCoreQueryableExtensions
    {
        public static IQueryable<Member> IncludeDetails(this IQueryable<Member> queryable, bool include = true)
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