using DocumentFormat.OpenXml.Math;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Linq.Dynamic.Core;
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
        var query = await DbContext.Set<Question>()
                    .WhereIf(questionCategoryIds != null ,x => questionCategoryIds.Contains(x.QuestionCategoryId))
                    .WhereIf((questionType.HasValue), x => x.Type == questionType)
                    .ToListAsync();
       // TODO: 优化在数据库中分组查询 将方法封装到领域服务中             
       var list =   query.GroupBy(x => new { x.QuestionCategoryId }).Select(x => new DifferentDegreeQuestionCountInfo
                    {
                        QuestionCategoryId = x.Key.QuestionCategoryId,
                        UnlimitedDifficultyCount = x.Count(g => g.Degree == QuestionDegree.UnlimitedDifficulty),
                        SimpleCount = x.Count(g => g.Degree == QuestionDegree.Simple),
                        OrdinaryCount = x.Count(g => g.Degree == QuestionDegree.Ordinary),
                        DifficultCount = x.Count(g => g.Degree == QuestionDegree.Difficult)
                    }).ToList();
        return list;
    }

    /// <summary>
    /// 查询指定数量的随机题目
    /// </summary>
    /// <param name="questionCateogryId"></param>
    /// <param name="questionType"></param>
    /// <param name="questionDegree"></param>
    /// <param name="selectQuestionCount"></param>
    /// <returns></returns>
    public async Task<List<Question>> GetRandomList(Guid? questionCateogryId, QuestionType? questionType,
        QuestionDegree? questionDegree, int selectQuestionCount)
    {
        var query = await DbContext.Set<Question>()
                                .WhereIf(questionCateogryId.HasValue, x => x.QuestionCategoryId == questionCateogryId)
                                .WhereIf(questionType.HasValue, x => x.Type == questionType)
                                .WhereIf(questionDegree.HasValue, x => x.Degree == questionDegree)
                                //OrderBy(a =＞ Guid.NewGuid()) 随机排序 https://blog.csdn.net/weixin_43942765/article/details/121810450
                                .OrderBy(x => Guid.NewGuid())
                                .Take(selectQuestionCount).ToListAsync();
        return query;
    }

    public override  IQueryable<Question> WithDetails()
    {
        return  GetQueryable().IncludeDetails();
    }
}