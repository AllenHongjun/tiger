using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Exams;


/// <summary>
/// 试卷
/// </summary>
public class TestPaperAppService : CrudAppService<TestPaper, TestPaperDto, Guid, TestPaperGetListInput, CreateUpdateTestPaperDto, CreateUpdateTestPaperDto>,
    ITestPaperAppService
{
   

    private readonly ITestPaperRepository _repository;

    public TestPaperAppService(ITestPaperRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override  IQueryable<TestPaper> CreateFilteredQuery(TestPaperGetListInput input)
    {
        // TODO: AbpHelper generated
        return base.CreateFilteredQuery(input)
            .WhereIf(input.TestPaperMainId != null, x => x.TestPaperMainId == input.TestPaperMainId)
            .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(input.Type != null, x => x.Type == input.Type)
            .WhereIf(input.IsComposing != null, x => x.IsComposing == input.IsComposing)
            .WhereIf(input.Enable != null, x => x.Enable == input.Enable)
            .WhereIf(input.IsIncludeAllSchoolTeachers != null, x => x.IsIncludeAllSchoolTeachers == input.IsIncludeAllSchoolTeachers)
            .WhereIf(input.IsLimitJudgeTime != null, x => x.IsLimitJudgeTime == input.IsLimitJudgeTime)
            .WhereIf(input.JudgeStartTime != null, x => x.JudgeStartTime == input.JudgeStartTime)
            .WhereIf(input.JudgeEndTime != null, x => x.JudgeEndTime == input.JudgeEndTime)
            ;
    }
}
