using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;

namespace Tiger.Module.Exams;


/// <summary>
/// 考试
/// </summary>
[RemoteService(false)]
public class ExamAppService : CrudAppService<Exam, ExamDto, Guid, ExamGetListInput, CreateUpdateExamDto, CreateUpdateExamDto>,
    IExamAppService
{
    protected override string GetPolicyName { get; set; } = TigerPermissions.Exam.Default;
    protected override string GetListPolicyName { get; set; } = TigerPermissions.Exam.Default;
    protected override string CreatePolicyName { get; set; } = TigerPermissions.Exam.Create;
    protected override string UpdatePolicyName { get; set; } = TigerPermissions.Exam.Update;
    protected override string DeletePolicyName { get; set; } = TigerPermissions.Exam.Delete;

    private readonly IExamRepository _repository;

    public ExamAppService(IExamRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override  IQueryable<Exam> CreateFilteredQuery(ExamGetListInput input)
    {
        // TODO: AbpHelper generated
        return base.CreateFilteredQuery(input)
            .WhereIf(input.CourseId != null, x => x.CourseId == input.CourseId)
            .WhereIf(input.TestPaperId != null, x => x.TestPaperId == input.TestPaperId)
            .WhereIf(input.QuestionCategoryId != null, x => x.QuestionCategoryId == input.QuestionCategoryId)
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(!input.CoverUrl.IsNullOrWhiteSpace(), x => x.CoverUrl.Contains(input.CoverUrl))
            .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(input.ExamType != null, x => x.ExamType == input.ExamType)
            .WhereIf(input.StartDate != null, x => x.StartDate == input.StartDate)
            .WhereIf(input.EndDate != null, x => x.EndDate == input.EndDate)
            .WhereIf(input.ExamDuration != null, x => x.ExamDuration == input.ExamDuration)
            .WhereIf(input.IsDifferent != null, x => x.IsDifferent == input.IsDifferent)
            .WhereIf(input.IsDifferentOrder != null, x => x.IsDifferentOrder == input.IsDifferentOrder)
            .WhereIf(input.IsShowScore != null, x => x.IsShowScore == input.IsShowScore)
            .WhereIf(input.IsShowError != null, x => x.IsShowError == input.IsShowError)
            .WhereIf(input.IsEnable != null, x => x.IsEnable == input.IsEnable)
            .WhereIf(input.IsExamAnyTime != null, x => x.IsExamAnyTime == input.IsExamAnyTime)
            .WhereIf(input.IsShowWindowOnblur != null, x => x.IsShowWindowOnblur == input.IsShowWindowOnblur)
            .WhereIf(input.MaxExamCount != null, x => x.MaxExamCount == input.MaxExamCount)
            .WhereIf(input.OnlyExamDayVisible != null, x => x.OnlyExamDayVisible == input.OnlyExamDayVisible)
            .WhereIf(input.IsStartSync != null, x => x.IsStartSync == input.IsStartSync)
            .WhereIf(input.IsShowHelp != null, x => x.IsShowHelp == input.IsShowHelp)
            .WhereIf(input.HalftimeFlag != null, x => x.HalftimeFlag == input.HalftimeFlag)
            .WhereIf(input.HalftimeStart != null, x => x.HalftimeStart == input.HalftimeStart)
            .WhereIf(input.HalftimeEnd != null, x => x.HalftimeEnd == input.HalftimeEnd)
            .WhereIf(input.DeductionAmounnt != null, x => x.DeductionAmounnt == input.DeductionAmounnt)
            .WhereIf(input.DeductionInterval != null, x => x.DeductionInterval == input.DeductionInterval)
            .WhereIf(input.Interval != null, x => x.Interval == input.Interval)
            ;
    }
}
