using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.Exams;

/// <summary>
/// 组卷策略配置表
/// </summary>
public static class TestPaperStrategyEfCoreQueryableExtensions
{
    public static IQueryable<TestPaperStrategy> IncludeDetails(this IQueryable<TestPaperStrategy> queryable, bool include = true)
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
