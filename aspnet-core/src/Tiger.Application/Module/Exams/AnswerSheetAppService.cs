using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Exams;


/// <summary>
/// 答卷表
/// </summary>
public class AnswerSheetAppService : CrudAppService<AnswerSheet, AnswerSheetDto, Guid, AnswerSheetGetListInput, CreateUpdateAnswerSheetDto, CreateUpdateAnswerSheetDto>,
    IAnswerSheetAppService
{

    private readonly IAnswerSheetRepository _repository;

    public AnswerSheetAppService(IAnswerSheetRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override IQueryable<AnswerSheet> CreateFilteredQuery(AnswerSheetGetListInput input)
    {
        // TODO: AbpHelper generated
        return  base.CreateFilteredQuery(input)
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
}
