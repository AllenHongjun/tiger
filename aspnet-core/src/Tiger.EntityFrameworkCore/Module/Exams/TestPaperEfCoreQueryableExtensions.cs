using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.Exams;

/// <summary>
/// 试卷
/// </summary>
public static class TestPaperEfCoreQueryableExtensions
{
    public static IQueryable<TestPaper> IncludeDetails(this IQueryable<TestPaper> queryable, bool include = true)
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
