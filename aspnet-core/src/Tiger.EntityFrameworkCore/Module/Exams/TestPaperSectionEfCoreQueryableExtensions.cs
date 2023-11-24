using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.Exams;

/// <summary>
/// 试卷大题
/// </summary>
public static class TestPaperSectionEfCoreQueryableExtensions
{
    public static IQueryable<TestPaperSection> IncludeDetails(this IQueryable<TestPaperSection> queryable, bool include = true)
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
