using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.QuestionBank;

/// <summary>
/// 题目答案
/// </summary>
public interface IQuestionAnswerRepository : IRepository<QuestionAnswer, Guid>
{
}
