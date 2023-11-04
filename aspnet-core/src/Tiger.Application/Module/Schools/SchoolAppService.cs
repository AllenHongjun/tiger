using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.Schools.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Schools;


/// <summary>
/// 学校管理
/// </summary>
public class SchoolAppService : CrudAppService<School, SchoolDto, Guid, SchoolGetListInput, CreateUpdateSchoolDto, CreateUpdateSchoolDto>,
    ISchoolAppService
{
    protected override string GetPolicyName { get; set; } = TigerPermissions.School.Default;
    protected override string GetListPolicyName { get; set; } = TigerPermissions.School.Default;
    protected override string CreatePolicyName { get; set; } = TigerPermissions.School.Create;
    protected override string UpdatePolicyName { get; set; } = TigerPermissions.School.Update;
    protected override string DeletePolicyName { get; set; } = TigerPermissions.School.Delete;

    private readonly ISchoolRepository _repository;

    public SchoolAppService(ISchoolRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override IQueryable<School> CreateFilteredQuery(SchoolGetListInput input)
    {
        // TODO: AbpHelper generated
        return (base.CreateFilteredQuery(input))
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(!input.ShortName.IsNullOrWhiteSpace(), x => x.ShortName.Contains(input.ShortName))
            .WhereIf(input.Sort != null, x => x.Sort == input.Sort)
            .WhereIf(input.IsEnable != null, x => x.IsEnable == input.IsEnable)
            .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(input.ImpowerDate != null, x => x.ImpowerDate == input.ImpowerDate)
            .WhereIf(input.MaxPerson != null, x => x.MaxPerson == input.MaxPerson)
            .WhereIf(input.IsAudit != null, x => x.IsAudit == input.IsAudit)
            .WhereIf(input.VipLevel != null, x => x.VipLevel == input.VipLevel)
            ;
    }
}
