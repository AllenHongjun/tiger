using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.QuestionBank;

/// <summary>
/// 题目答案
/// </summary>
public static class QuestionAnswerEfCoreQueryableExtensions
{
    public static IQueryable<QuestionAnswer> IncludeDetails(this IQueryable<QuestionAnswer> queryable, bool include = true)
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
