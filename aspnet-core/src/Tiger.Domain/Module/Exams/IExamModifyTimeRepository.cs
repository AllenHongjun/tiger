using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.Exams;

/// <summary>
/// 考试时间调整表
/// </summary>
public interface IExamModifyTimeRepository : IRepository<ExamModifyTime, Guid>
{
}
