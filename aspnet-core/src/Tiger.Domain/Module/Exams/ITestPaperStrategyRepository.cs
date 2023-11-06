using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.Exams;

/// <summary>
/// 组卷策略配置表
/// </summary>
public interface ITestPaperStrategyRepository : IRepository<TestPaperStrategy, Guid>
{
}
