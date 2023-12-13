using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.QuestionBank;

/// <summary>
/// 实训平台
/// </summary>
public static class TrainPlatformEfCoreQueryableExtensions
{
    public static IQueryable<TrainPlatform> IncludeDetails(this IQueryable<TrainPlatform> queryable, bool include = true)
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
