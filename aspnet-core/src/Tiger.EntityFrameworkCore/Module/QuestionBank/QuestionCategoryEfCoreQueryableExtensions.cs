using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.QuestionBank;

/// <summary>
/// 题目分类
/// </summary>
public static class QuestionCategoryEfCoreQueryableExtensions
{
    public static IQueryable<QuestionCategory> IncludeDetails(this IQueryable<QuestionCategory> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Children) // TODO: AbpHelper generated
            .Include(x => x.Questions)
            ;
    }
}
