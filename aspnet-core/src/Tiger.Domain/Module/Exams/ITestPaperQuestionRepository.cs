using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.Exams;

/// <summary>
/// 试卷内容(题目)表
/// </summary>
public interface ITestPaperQuestionRepository : IRepository<TestPaperQuestion, Guid>
{
}
