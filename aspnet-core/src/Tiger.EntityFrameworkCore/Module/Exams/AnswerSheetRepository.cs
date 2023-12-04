using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.SettingManagement;

namespace Tiger.Module.Exams;

public class AnswerSheetRepository : EfCoreRepository<TigerDbContext, AnswerSheet, Guid>, IAnswerSheetRepository
{
    public AnswerSheetRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    /// <summary>
    /// 成绩统计
    /// </summary>
    /// <param name="examId"></param>
    /// <param name="sorting"></param>
    /// <param name="maxResultCount"></param>
    /// <param name="skipCount"></param>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// 统计一场考试的考生成绩(一场考试同一考生可以参加多次)
    /// </remarks>
    public async Task<List<ExamScoreAnalysisInfo>> GetExamScoreAnalysisAsync(Guid? examId, string sorting = null, int maxResultCount = 50, int skipCount = 0,
            string filter = null, CancellationToken cancellationToken = default)
    {
        var query = from exam in DbContext.Set<Exam>()
                    join ans in DbContext.Set<AnswerSheet>()
                        on exam.Id equals ans.ExamId
                    
                    select new { exam, ans };

        query = query.WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.exam.Name.Contains(filter))
                .WhereIf(examId.HasValue, x => x.ans.ExamId == examId);

        var items =   from q in query
                      group q by new { q.ans.CreatorId } into g
                      select new ExamScoreAnalysisInfo
                      {
                          CreatorId = g.Key.CreatorId.Value,
                          NumberOfParticipates = g.Count(),
                          AverageExamDuration = g.Average(x => x.ans.ExamDuration),
                          HighestTotalScore = g.Max(x => x.ans.TotalScore),
                          AverageTotalScore = g.Average(x => x.ans.TotalScore),
                          LowestTotalScore = g.Min(x => x.ans.TotalScore),
                          IsPass = true, // TODO:查询列表用户是否及格
                      };
         return await items.OrderBy(sorting.IsNullOrEmpty() ? nameof(ExamScoreAnalysisInfo.CreatorId) : sorting)
                                .PageBy(skipCount, maxResultCount)
                                .ToListAsync(GetCancellationToken(cancellationToken));
        

    }

    public override IQueryable<AnswerSheet> WithDetails()
    {
        return GetQueryable().IncludeDetails();
    }
}