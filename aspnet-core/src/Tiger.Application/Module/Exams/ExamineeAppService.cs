using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Exams;


/// <summary>
/// 考试人员表(应试人；参加考试者)
/// </summary>
public class ExamineeAppService : CrudAppService<Examinee, ExamineeDto, Guid, ExamineeGetListInput, CreateUpdateExamineeDto, CreateUpdateExamineeDto>,
    IExamineeAppService
{
    protected override string GetPolicyName { get; set; } = TigerPermissions.Examinee.Default;
    protected override string GetListPolicyName { get; set; } = TigerPermissions.Examinee.Default;
    protected override string CreatePolicyName { get; set; } = TigerPermissions.Examinee.Create;
    protected override string UpdatePolicyName { get; set; } = TigerPermissions.Examinee.Update;
    protected override string DeletePolicyName { get; set; } = TigerPermissions.Examinee.Delete;

    private readonly IExamineeRepository _repository;

    public ExamineeAppService(IExamineeRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override IQueryable<Examinee> CreateFilteredQuery(ExamineeGetListInput input)
    {
        // TODO: AbpHelper generated
        return  base.CreateFilteredQuery(input)
            .WhereIf(input.ExamId != null, x => x.ExamId == input.ExamId)
            .WhereIf(input.UserId != null, x => x.UserId == input.UserId)
            .WhereIf(!input.UserName.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.UserName))
            .WhereIf(!input.FullName.IsNullOrWhiteSpace(), x => x.FullName.Contains(input.FullName))
            .WhereIf(!input.Email.IsNullOrWhiteSpace(), x => x.Email.Contains(input.Email))
            .WhereIf(!input.PhoneNumber.IsNullOrWhiteSpace(), x => x.PhoneNumber.Contains(input.PhoneNumber))
            .WhereIf(input.OrganizationUnitId != null, x => x.OrganizationUnitId == input.OrganizationUnitId)
            .WhereIf(!input.OrganizationUnitName.IsNullOrWhiteSpace(), x => x.OrganizationUnitName.Contains(input.OrganizationUnitName))
            ;
    }
}
