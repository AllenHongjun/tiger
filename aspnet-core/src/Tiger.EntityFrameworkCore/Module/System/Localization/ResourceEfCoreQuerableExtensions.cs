using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.System.Localization;

public static class ResourceEfCoreQueryableExtensions
{
    public static IQueryable<Resource> IncludeDetails(this IQueryable<Resource> queryable, bool include = true)
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
