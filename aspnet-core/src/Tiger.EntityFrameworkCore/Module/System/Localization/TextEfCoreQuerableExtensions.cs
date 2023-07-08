using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.System.Localization;

public static class TextEfCoreQueryableExtensions
{
    public static IQueryable<LanguageText> IncludeDetails(this IQueryable<LanguageText> queryable, bool include = true)
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
