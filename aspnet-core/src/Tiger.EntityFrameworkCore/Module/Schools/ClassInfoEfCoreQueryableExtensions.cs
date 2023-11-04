using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.Schools;

/// <summary>
/// 班级
/// </summary>
public static class ClassInfoEfCoreQueryableExtensions
{
    public static IQueryable<ClassInfo> IncludeDetails(this IQueryable<ClassInfo> queryable, bool include = true)
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
