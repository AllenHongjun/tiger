using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.Exams;

/// <summary>
/// 试卷大题
/// </summary>
public interface ITestPaperSectionRepository : IRepository<TestPaperSection, Guid>
{
}
