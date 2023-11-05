using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.QuestionBank;

/// <summary>
/// 题目表
/// </summary>
public interface IQuestionRepository : IRepository<Question, Guid>
{
}
