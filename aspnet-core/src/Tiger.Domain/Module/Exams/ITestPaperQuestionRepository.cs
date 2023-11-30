using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.Exams;

/// <summary>
/// 试卷内容(题目)表
/// </summary>
public interface ITestPaperQuestionRepository : IRepository<TestPaperQuestion, Guid>
{
    Task<List<TestPaperQuestion>> GetAllListAsync(Guid? testPaperSectionId, string sorting = null, CancellationToken cancellationToken = default);
}
