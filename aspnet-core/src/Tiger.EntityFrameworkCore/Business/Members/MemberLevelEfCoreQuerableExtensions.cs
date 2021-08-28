using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Business.Members
{
    public static class MemberLevelEfCoreQueryableExtensions
    {
        public static IQueryable<MemberLevel> IncludeDetails(this IQueryable<MemberLevel> queryable, bool include = true)
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