using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiger.Module.QuestionBank;

/// <summary>
/// 题目附件表
/// </summary>
public static class QuestionAttachmentEfCoreQueryableExtensions
{
    public static IQueryable<QuestionAttachment> IncludeDetails(this IQueryable<QuestionAttachment> queryable, bool include = true)
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
