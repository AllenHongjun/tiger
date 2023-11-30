using DocumentFormat.OpenXml.Math;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.QuestionBank;

public class QuestionRepository : EfCoreRepository<TigerDbContext, Question, Guid>, IQuestionRepository
{
    public QuestionRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    /// <summary>
    /// 根据题目分类查询不同难度题目数量
    /// </summary>
    /// <param name="questionCategoryIds"></param>
    /// <returns></returns>
    public async Task<List<DifferentDegreeQuestionCountInfo>> GetDifferentDegreeQuestionCount(List<Guid> questionCategoryIds, QuestionType? questionType)
    {
        // bug: 自EF core 3开始，LINQ查询命令无法转换成SQL语句 https://blog.csdn.net/connora/article/details/108313146
        var query = DbContext.Set<Question>()
                    .Where(x => questionCategoryIds.Contains(x.QuestionCategoryId))
                    .WhereIf((questionType.HasValue), x => x.Type == questionType)
                    .AsEnumerable();
       // TODO: 优化在数据库中分组查询 将方法封装到领域服务中             
       var list =   query.GroupBy(x => new { x.QuestionCategoryId }).Select(x => new DifferentDegreeQuestionCountInfo
                    {
                        QuestionCategoryId = x.Key.QuestionCategoryId,
                        UnlimitedDifficultyCount = x.Count(g => g.Degree == QuestionDegree.UnlimitedDifficultyCount),
                        SimpleCount = x.Count(g => g.Degree == QuestionDegree.Simple),
                        OrdinaryCount = x.Count(g => g.Degree == QuestionDegree.Ordinary),
                        DifficultCount = x.Count(g => g.Degree == QuestionDegree.Difficult)
                    }).ToList();
        return list;
    }

    public override  IQueryable<Question> WithDetails()
    {
        return  GetQueryable().IncludeDetails();
    }
}