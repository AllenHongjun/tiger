using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.Exams;

/// <summary>
/// 考试时间调整表
/// </summary>
public static class ExamModifyTimeEfCoreQueryableExtensions
{
    public static IQueryable<ExamModifyTime> IncludeDetails(this IQueryable<ExamModifyTime> queryable, bool include = true)
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
