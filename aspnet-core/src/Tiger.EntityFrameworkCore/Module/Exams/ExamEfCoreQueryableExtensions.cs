using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.Exams;

/// <summary>
/// 考试
/// </summary>
public static class ExamEfCoreQueryableExtensions
{
    public static IQueryable<Exam> IncludeDetails(this IQueryable<Exam> queryable, bool include = true)
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
