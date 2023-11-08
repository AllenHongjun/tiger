using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.Exams;

/// <summary>
/// 答卷表
/// </summary>
public interface IAnswerSheetRepository : IRepository<AnswerSheet, Guid>
{
}
