using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tiger.Module.Train;

namespace Tiger.Module.Teachings;

/// <summary>
/// 课程
/// </summary>
public static class CourseEfCoreQueryableExtensions
{
    public static IQueryable<Course> IncludeDetails(this IQueryable<Course> queryable, bool include = true)
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
