using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.Exams;

/// <summary>
/// 考试
/// </summary>
public interface IExamRepository : IRepository<Exam, Guid>
{
}
