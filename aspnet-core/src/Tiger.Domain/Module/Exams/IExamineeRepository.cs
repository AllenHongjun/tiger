using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.Exams;

/// <summary>
/// 考试人员表(应试人；参加考试者)
/// </summary>
public interface IExamineeRepository : IRepository<Examinee, Guid>
{
}
