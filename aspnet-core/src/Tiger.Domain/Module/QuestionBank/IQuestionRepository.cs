using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.QuestionBank;

/// <summary>
/// 题目表
/// </summary>
public interface IQuestionRepository : IRepository<Question, Guid>
{
    Task<List<DifferentDegreeQuestionCountInfo>> GetDifferentDegreeQuestionCount(List<Guid> questionCategoryIds, QuestionType? questionType);
    Task<List<Question>> GetRandomList(Guid? questionCateogryId, QuestionType? questionType, QuestionDegree? questionDegree, int selectQuestionCount);
}
