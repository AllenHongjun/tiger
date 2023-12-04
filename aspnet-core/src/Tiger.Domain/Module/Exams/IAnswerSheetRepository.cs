using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.Exams;

/// <summary>
/// 答卷表
/// </summary>
public interface IAnswerSheetRepository : IRepository<AnswerSheet, Guid>
{
    Task<List<ExamScoreAnalysisInfo>> GetExamScoreAnalysisAsync(Guid? examId, string sorting = null, int maxResultCount = 50, int skipCount = 0, string filter = null, CancellationToken cancellationToken = default);
}
