using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;

namespace Tiger.Module.Exams;


/// <summary>
/// 试卷评委表        <remarks>    评卷人只有关联了试卷才能改卷（默认只有超管能改卷）    </remarks>
/// </summary>
[RemoteService( IsEnabled = false )]
public class TestPaperJudgeAppService : CrudAppService<TestPaperJudge, TestPaperJudgeDto, Guid, TestPaperJudgeGetListInput, CreateUpdateTestPaperJudgeDto, CreateUpdateTestPaperJudgeDto>,
    ITestPaperJudgeAppService
{
    protected override string GetPolicyName { get; set; } = TigerPermissions.TestPaperJudge.Default;
    protected override string GetListPolicyName { get; set; } = TigerPermissions.TestPaperJudge.Default;
    protected override string CreatePolicyName { get; set; } = TigerPermissions.TestPaperJudge.Create;
    protected override string UpdatePolicyName { get; set; } = TigerPermissions.TestPaperJudge.Update;
    protected override string DeletePolicyName { get; set; } = TigerPermissions.TestPaperJudge.Delete;

    private readonly ITestPaperJudgeRepository _repository;

    public TestPaperJudgeAppService(ITestPaperJudgeRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override  IQueryable<TestPaperJudge> CreateFilteredQuery(TestPaperJudgeGetListInput input)
    {
        // TODO: AbpHelper generated
        return  base.CreateFilteredQuery(input)
            .WhereIf(input.TestPaperId != null, x => x.TestPaperId == input.TestPaperId)
            .WhereIf(input.JudgeId != null, x => x.JudgeId == input.JudgeId)
            .WhereIf(!input.UserName.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.UserName))
            .WhereIf(!input.FullName.IsNullOrWhiteSpace(), x => x.FullName.Contains(input.FullName))
            .WhereIf(!input.Email.IsNullOrWhiteSpace(), x => x.Email.Contains(input.Email))
            .WhereIf(!input.PhoneNumber.IsNullOrWhiteSpace(), x => x.PhoneNumber.Contains(input.PhoneNumber))
            .WhereIf(input.OrganizationUnitId != null, x => x.OrganizationUnitId == input.OrganizationUnitId)
            ;
    }
}
