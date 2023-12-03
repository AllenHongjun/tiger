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
            .WhereIf(input.TestPaperMainId != null, x => x.TestPaperMainId == input.TestPaperMainId)
            .WhereIf(input.TestPaperId != null, x => x.TestPaperId == input.TestPaperId)
            .WhereIf(input.ExamId != null, x => x.ExamId == input.ExamId)
            .WhereIf(input.StudentId != null, x => x.StudentId == input.StudentId)
            .WhereIf(input.TotalScore != null, x => x.TotalScore == input.TotalScore)
            .WhereIf(input.IsSubmit != null, x => x.IsSubmit == input.IsSubmit)
            .WhereIf(input.SubmitDateTime != null, x => x.SubmitDateTime == input.SubmitDateTime)
            .WhereIf(!input.IP.IsNullOrWhiteSpace(), x => x.IP.Contains(input.IP))
            .WhereIf(input.DeviceType != null, x => x.DeviceType == input.DeviceType)
            .WhereIf(input.ExamDuration != null, x => x.ExamDuration == input.ExamDuration)
            .WhereIf(input.AnswerTotalDuration != null, x => x.AnswerTotalDuration == input.AnswerTotalDuration)
            .WhereIf(input.WindowOnblur != null, x => x.WindowOnblur == input.WindowOnblur)
            .WhereIf(input.ScoreTime != null, x => x.ScoreTime == input.ScoreTime)
            .WhereIf(input.OperateAutoScore != null, x => x.OperateAutoScore == input.OperateAutoScore)
            .WhereIf(input.OperateAutoScoreTime != null, x => x.OperateAutoScoreTime == input.OperateAutoScoreTime)
            .WhereIf(input.OperateManualScore != null, x => x.OperateManualScore == input.OperateManualScore)
            .WhereIf(input.OperateManualScoreTime != null, x => x.OperateManualScoreTime == input.OperateManualScoreTime)
            .WhereIf(input.ObjectiveScore != null, x => x.ObjectiveScore == input.ObjectiveScore)
            .WhereIf(input.ObjectiveScoreTime != null, x => x.ObjectiveScoreTime == input.ObjectiveScoreTime)
            ;
    }

    public override async Task<PagedResultDto<AnswerSheetDto>> GetListAsync(AnswerSheetGetListInput input)
    {
        var answerSheets = await base.GetListAsync(input);

        // 二次查询数据
        List<Guid> creatorIds = answerSheets.Items.Where(x => x.CreatorId != null).Select(x => (Guid)x.CreatorId).ToList();
        var creators = await _userRepository.GetListByIdListAsync(creatorIds);

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

    #endregion
}
