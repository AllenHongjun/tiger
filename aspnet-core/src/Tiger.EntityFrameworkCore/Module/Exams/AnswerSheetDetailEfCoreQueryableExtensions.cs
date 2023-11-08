using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.Exams;

/// <summary>
/// 答卷明细表
/// </summary>
public static class AnswerSheetDetailEfCoreQueryableExtensions
{
    public static IQueryable<AnswerSheetDetail> IncludeDetails(this IQueryable<AnswerSheetDetail> queryable, bool include = true)
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
