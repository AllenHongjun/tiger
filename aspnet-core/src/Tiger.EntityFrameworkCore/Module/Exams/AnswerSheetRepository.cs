using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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
                    where ans.ExamId == examId
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
                       NumberOfAnswers = answerSheetDetails.Count(x => x.QuestionId == g.Key.QuestionId),
                       NumberOfWrongAnswers = answerSheetDetails.Count(x => x.IsCorrect == false),
                       ErrorRate = answerSheetDetails.Count(x => x.IsCorrect.Value == false) / answerSheetDetails.Count(x => x.QuestionId == g.Key.QuestionId),
                       NumberOfRightAnswers = answerSheetDetails.Count(x => x.IsCorrect == true),
                       CorrectRate = answerSheetDetails.Count(x => x.IsCorrect.Value == false) / answerSheetDetails.Count(x => x.QuestionId == g.Key.QuestionId),
                       ScoringRate = answerSheetDetails.Sum(x => x.TotalScore) / g.Sum(x => x.question.Score)
                    };

        return await items.OrderBy(sorting.IsNullOrEmpty() ? nameof(QuestionAnalysisInfo.Degree) : sorting)
                                .PageBy(skipCount, maxResultCount)
                                .ToListAsync(GetCancellationToken(cancellationToken));
    }





    /// <summary>
    /// 题目类型分析
    /// </summary>
    /// <param name="questionCategoryId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <remarks>
    /// 根据题目id查询每种题目类型不同难度的题目数量
    /// </remarks>
    public async Task<List<QuestionTypeAnalysisInfo>> GetQuestionTypeAnalysisAsync(Guid? questionCategoryId, CancellationToken cancellationToken = default)
    {
        var query = (from  question in DbContext.Set<Question>()
                    select question)
                    .WhereIf(questionCategoryId.HasValue, x => x.QuestionCategoryId == questionCategoryId);

        var items = from q in query
                    group q by new { q.Type } into g
                    select new QuestionTypeAnalysisInfo
                    {
                        Type = g.Key.Type,
                        QuestionCount = g.Count(),
                        UnlimitedDifficultyCount = g.Count(x => x.Degree == QuestionDegree.UnlimitedDifficulty),
                        SimpleCount = g.Count(x => x.Degree == QuestionDegree.Simple),
                        OrdinaryCount = g.Count(x => x.Degree == QuestionDegree.Ordinary),
                        DifficultCount = g.Count(x => x.Degree == QuestionDegree.Difficult)
                    };

        return await items.ToListAsync(GetCancellationToken(cancellationToken));
    }

    public override IQueryable<AnswerSheet> WithDetails()
    {
        return GetQueryable().IncludeDetails();
    }
}