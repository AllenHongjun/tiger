using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.Exams;

/// <summary>
/// 试卷
/// </summary>
public interface ITestPaperRepository : IRepository<TestPaper, Guid>
{
}
