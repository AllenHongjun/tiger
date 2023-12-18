using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.QuestionBank;

/// <summary>
/// 题目分类
/// </summary>
public interface IQuestionCategoryRepository : IRepository<QuestionCategory, Guid>
{
}
