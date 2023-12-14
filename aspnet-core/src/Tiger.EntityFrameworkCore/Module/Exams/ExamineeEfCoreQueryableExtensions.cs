using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.Exams;

/// <summary>
/// 考试人员表(应试人；参加考试者)
/// </summary>
public static class ExamineeEfCoreQueryableExtensions
{
    public static IQueryable<Examinee> IncludeDetails(this IQueryable<Examinee> queryable, bool include = true)
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
