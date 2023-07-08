using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.System.Localization;

public static class LanguageEfCoreQueryableExtensions
{
    public static IQueryable<Language> IncludeDetails(this IQueryable<Language> queryable, bool include = true)
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
