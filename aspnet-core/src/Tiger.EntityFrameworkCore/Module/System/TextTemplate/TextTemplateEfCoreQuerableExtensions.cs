using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.System.TextTemplate;

public static class TextTemplateEfCoreQueryableExtensions
{
    public static IQueryable<TextTemplate> IncludeDetails(this IQueryable<TextTemplate> queryable, bool include = true)
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
