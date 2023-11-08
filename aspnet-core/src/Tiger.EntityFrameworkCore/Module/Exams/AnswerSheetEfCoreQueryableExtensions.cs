using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.Exams;

/// <summary>
/// 答卷表
/// </summary>
public static class AnswerSheetEfCoreQueryableExtensions
{
    public static IQueryable<AnswerSheet> IncludeDetails(this IQueryable<AnswerSheet> queryable, bool include = true)
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
