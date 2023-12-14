using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.Exams;

/// <summary>
/// 试卷评委表        <remarks>    评卷人只有关联了试卷才能改卷（默认只有超管能改卷）    </remarks>
/// </summary>
public static class TestPaperJudgeEfCoreQueryableExtensions
{
    public static IQueryable<TestPaperJudge> IncludeDetails(this IQueryable<TestPaperJudge> queryable, bool include = true)
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
