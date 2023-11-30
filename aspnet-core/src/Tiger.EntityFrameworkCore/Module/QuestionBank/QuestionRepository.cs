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
    /// ������Ŀ�����ѯ��ͬ�Ѷ���Ŀ����
    /// </summary>
    /// <param name="questionCategoryIds"></param>
    /// <returns></returns>
    public async Task<List<DifferentDegreeQuestionCountInfo>> GetDifferentDegreeQuestionCount(List<Guid> questionCategoryIds, QuestionType? questionType)
    {
        // bug: ��EF core 3��ʼ��LINQ��ѯ�����޷�ת����SQL��� https://blog.csdn.net/connora/article/details/108313146
        var query = DbContext.Set<Question>()
                    .Where(x => questionCategoryIds.Contains(x.QuestionCategoryId))
                    .WhereIf((questionType.HasValue), x => x.Type == questionType)
                    .AsEnumerable();
       // TODO: �Ż������ݿ��з����ѯ ��������װ�����������             
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