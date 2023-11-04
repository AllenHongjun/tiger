using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.Schools;

/// <summary>
/// 学校管理
/// </summary>
public static class SchoolEfCoreQueryableExtensions
{
    public static IQueryable<School> IncludeDetails(this IQueryable<School> queryable, bool include = true)
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
