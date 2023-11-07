using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.Exams;

/// <summary>
/// 试卷内容(题目)表
/// </summary>
public static class TestPaperQuestionEfCoreQueryableExtensions
{
    public static IQueryable<TestPaperQuestion> IncludeDetails(this IQueryable<TestPaperQuestion> queryable, bool include = true)
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
