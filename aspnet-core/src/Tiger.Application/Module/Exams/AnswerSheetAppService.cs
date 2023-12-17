using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Volo.Abp.Users;
using Volo.Abp.Identity;
using Tiger.Volo.Abp.Identity;
using System.Threading;
using Volo.Abp.ObjectMapping;
using Tiger.Module.QuestionBank.Dtos;

namespace Tiger.Module.Exams;


/// <summary>
/// 答卷表
/// </summary>
[RemoteService(IsEnabled = false)]
public class AnswerSheetAppService : CrudAppService<AnswerSheet, AnswerSheetDto, Guid, AnswerSheetGetListInput, CreateUpdateAnswerSheetDto, CreateUpdateAnswerSheetDto>,
    IAnswerSheetAppService
{

    #region 构造函数和字段
    private readonly IAnswerSheetRepository _repository;
    private readonly ITigerIdentityUserRepository _userRepository;
    private readonly IExamRepository _examRepository;

    public AnswerSheetAppService(
        IAnswerSheetRepository repository,
        ITigerIdentityUserRepository userRepository,
        IExamRepository examRepository) : base(repository)
    {
        _repository = repository;
        _userRepository=userRepository;
        _examRepository=examRepository;
    }
    #endregion

    #region CRUD
    protected override IQueryable<AnswerSheet> CreateFilteredQuery(AnswerSheetGetListInput input)
    {
        // TODO: AbpHelper generated
        return base.CreateFilteredQuery(input)
            .WhereIf(input.ExamId != null, x => x.ExamId == input.ExamId)
            .WhereIf(input.CreateStartTime != null, x => x.CreationTime >= input.CreateStartTime)
            .WhereIf(input.CreateEndTime != null, x => x.CreationTime <= input.CreateEndTime)
            .WhereIf(input.AnswerSheetStatus.HasValue, x => x.Status == input.AnswerSheetStatus)
            .WhereIf(input.OrganizationUnitId.HasValue, x => x.OrganizationUnitId == input.OrganizationUnitId)
            .WhereIf(input.IsPass.HasValue, x => x.IsPass == input.IsPass)
            ;
    }

    public override async Task<PagedResultDto<AnswerSheetDto>> GetListAsync(AnswerSheetGetListInput input)
    {
        var answerSheets = await base.GetListAsync(input);

        // 二次查询数据
        List<Guid> creatorIds = answerSheets.Items.Where(x => x.CreatorId != null).Select(x => (Guid)x.CreatorId).ToList();
        var creators = await _userRepository.GetListByIdsAsync(creatorIds);

        List<Guid> examIds = answerSheets.Items.Select(x => x.ExamId).ToList();
        List<Exam> exams = _examRepository.Where(x => examIds.Contains(x.Id)).ToList();
        foreach (var answerSheet in answerSheets.Items)
        {
            var creator = creators.FirstOrDefault(x => x.Id == answerSheet.CreatorId);
            var exam = exams.FirstOrDefault(x => x.Id == answerSheet.ExamId);

            answerSheet.CreatorUserName = creator?.UserName;
            answerSheet.PassingScore = exam?.PassingScore; // 如果对象为NULL，则不进行后面的获取成员的运算，直接返回NULL
        }
        return answerSheets;
    } 

    /// <summary>
    /// 获取考生成绩面板数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public ExamScorePanelDto GetExamScorePanelData(Guid examId)
    {
        var answerSheets =  _repository.Where(x => x.ExamId == examId).ToList();
        var examScorePanelData = new ExamScorePanelDto()
        {
            NumberOfParticipants = answerSheets.Count(x => x.CreatorId != null),
            NumberOfPasses = answerSheets.Count(x => x.IsPass.HasValue && x.IsPass.Value == true),
            NumberOfFailedPasses =answerSheets.Count(x => x.IsPass.HasValue && x.IsPass.Value == false),
            ScoringRate = answerSheets.Sum(x => x.TotalScore) / 1000,
            HighestScore = answerSheets.Max(x => x.TotalScore),
            AverageScore = answerSheets.Average(x => x.TotalScore),
            LowestScore = answerSheets.Min(x => x.TotalScore)
        };
        return examScorePanelData;
    }

    /// <summary>
    /// 成绩统计
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<List<ExamScoreAnalysisDto>> GetExamScoreAnalysisAsync(AnswerSheetGetListInput input)
    {
        var list = await _repository.GetExamScoreAnalysisAsync(input.ExamId,input.Sorting,input.MaxResultCount,input.SkipCount,input.Filter);
        return ObjectMapper.Map<List<ExamScoreAnalysisInfo>, List<ExamScoreAnalysisDto>>(list);
    }

    /// <summary>
    /// 答题统计
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<List<QuestionAnalysisDto>> GetQuestionAnalysisAsync(QuestionGetListInput input)
    {
        var list = await _repository.GetQuestionAnalysisAsync(input.ExamId,input.QuestionCategoryId,
            input.Type, input.Degree,  input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);
        return ObjectMapper.Map<List<QuestionAnalysisInfo>, List<QuestionAnalysisDto>>(list);
    }
    #endregion
}
