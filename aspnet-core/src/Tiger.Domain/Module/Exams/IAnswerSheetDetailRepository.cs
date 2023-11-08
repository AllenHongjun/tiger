using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.Exams;

/// <summary>
/// 答卷明细表
/// </summary>
public interface IAnswerSheetDetailRepository : IRepository<AnswerSheetDetail, Guid>
{
}
