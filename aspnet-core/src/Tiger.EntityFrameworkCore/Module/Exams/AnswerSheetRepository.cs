using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Parser;
using System.Threading;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Tiger.Module.QuestionBank;
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

    /// <summary>
    /// 答题统计
    /// </summary>
    /// <param name="examId"></param>
    /// <param name="questionCategoryId"></param>
    /// <param name="questionType"></param>
    /// <param name="questionDegree"></param>
    /// <param name="sorting"></param>
    /// <param name="maxResultCount"></param>
    /// <param name="skipCount"></param>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<QuestionAnalysisInfo>> GetQuestionAnalysisAsync(Guid? examId, Guid? questionCategoryId, 
        QuestionType? questionType,QuestionDegree? questionDegree, string sorting = null, int maxResultCount = 50, int skipCount = 0,
            string filter = null, CancellationToken cancellationToken = default)
    {
        // 查询本场考试的答卷明细
        var answerSheetDetails = (from ansd in DbContext.Set<AnswerSheetDetail>()
                    join ans in DbContext.Set<AnswerSheet>() on ansd.AnswerSheetId equals ans.Id
                    //where ans.ExamId == examId
                    select ansd).ToList();

        var query = from ans  in DbContext.Set<AnswerSheet>() 
                    join ansd in DbContext.Set<AnswerSheetDetail>() on ans.Id equals ansd.AnswerSheetId
                    join question in DbContext.Set<Question>() on ansd.QuestionId  equals question.Id
                    select new { ans,ansd,question };

        query = query.WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.question.Content.Contains(filter))
                .WhereIf(examId.HasValue, x => x.ans.ExamId == examId)
                .WhereIf(questionCategoryId.HasValue, x => x.question.QuestionCategoryId == questionCategoryId)
                .WhereIf(questionType.HasValue, x => x.question.Type == questionType)
                .WhereIf(questionDegree.HasValue, x => x.question.Degree == questionDegree);

        var items = from q in query
                   group q by new { q.ansd.QuestionId, q.question.Content, q.question.Degree,q.question.QuestionCategoryId,q.question.Answer } into g
                   select new QuestionAnalysisInfo
                   {
                       QuestionId = g.Key.QuestionId, 
                       QuestionContent = g.Key.Content,
                       QuestionCategoryId = g.Key.QuestionCategoryId,
                       Degree = g.Key.Degree,
                       Answer = g.Key.Answer,
                       QuestionTotalScores =  g.Sum(x => x.question.Score)
                   };

         var questionAnalyses =  await items.OrderBy(sorting.IsNullOrEmpty() ? nameof(QuestionAnalysisInfo.Degree) : sorting)
                                    .PageBy(skipCount, maxResultCount)
                                    .ToListAsync(GetCancellationToken(cancellationToken));

        foreach (var item in questionAnalyses)
        {
            item.NumberOfAnswers = answerSheetDetails.Count(x => x.QuestionId == item.QuestionId);
            item.NumberOfWrongAnswers = answerSheetDetails.Count(x => x.IsCorrect == false);
            item.ErrorRate = answerSheetDetails.Count(x => x.IsCorrect.Value == false) / answerSheetDetails.Count(x => x.QuestionId == item.QuestionId);
            item.NumberOfRightAnswers = answerSheetDetails.Count(x => x.IsCorrect == true);
            item.CorrectRate = answerSheetDetails.Count(x => x.IsCorrect.Value == true) / answerSheetDetails.Count(x => x.QuestionId == item.QuestionId);
            item.ScoringRate = answerSheetDetails.Sum(x => x.Score) / questionAnalyses.Sum(x => x.QuestionTotalScores);
        }
        return questionAnalyses;
    }


    /// <summary>
    /// 组织统计
    /// </summary>
    /// <param name="examId"></param>
    /// <param name="sorting"></param>
    /// <param name="maxResultCount"></param>
    /// <param name="skipCount"></param>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<OrganizationUnitAnalysisInfo>> GetOrganizationUnitAnalysisAsync(Guid? examId, string sorting = null, int maxResultCount = 50, int skipCount = 0,
            string filter = null, CancellationToken cancellationToken = default)
    {
        // 获取这场考试试卷的总分 查询这场考试关联试卷的的总分

        var query = from ans in DbContext.Set<AnswerSheet>()
                    select ans ;

        query = query.WhereIf(!string.IsNullOrWhiteSpace(filter), x => true)
                .WhereIf(examId.HasValue, x => x.ExamId == examId);

        var items = from q in query
                    group q by new { q.OrganizationUnitId } into g
                    select new OrganizationUnitAnalysisInfo
                    {
                        NumberPassed = g.Count(x => x.IsPass == true),
                        PassingRate = g.Count(x => x.IsPass == true) / g.Count(),
                        ScoringRate = g.Sum(x => x.TotalScore) / g.Sum(x => x.TotalScore), 
                        HighestScore = g.Max(x => x.TotalScore),
                        AverageScore = g.Average(x => x.TotalScore),
                        LowestScore = g.Min(x => x.TotalScore),
                    };

        var organizationUnitAnalysises = await items.OrderBy(sorting.IsNullOrEmpty() ? nameof(OrganizationUnitAnalysisInfo.OrganizationUnitName) : sorting)
                                   .PageBy(skipCount, maxResultCount)
                                   .ToListAsync(GetCancellationToken(cancellationToken));

        // 统计参考人员数据  查询考试人员列表

        // 关联查询 OrganizationUnitName

        return organizationUnitAnalysises;
    }

    public override IQueryable<AnswerSheet> WithDetails()
    {
        return GetQueryable().IncludeDetails();
    }
}