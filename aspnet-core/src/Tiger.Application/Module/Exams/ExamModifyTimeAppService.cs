using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;

namespace Tiger.Module.Exams;


/// <summary>
/// 考试时间调整表
/// </summary>
[RemoteService(IsEnabled = false)]
public class ExamModifyTimeAppService : CrudAppService<ExamModifyTime, ExamModifyTimeDto, Guid, ExamModifyTimeGetListInput, CreateUpdateExamModifyTimeDto, CreateUpdateExamModifyTimeDto>,
    IExamModifyTimeAppService
{
    protected override string GetPolicyName { get; set; } = TigerPermissions.ExamModifyTime.Default;
    protected override string GetListPolicyName { get; set; } = TigerPermissions.ExamModifyTime.Default;
    protected override string CreatePolicyName { get; set; } = TigerPermissions.ExamModifyTime.Create;
    protected override string UpdatePolicyName { get; set; } = TigerPermissions.ExamModifyTime.Update;
    protected override string DeletePolicyName { get; set; } = TigerPermissions.ExamModifyTime.Delete;

    private readonly IExamModifyTimeRepository _repository;

    public ExamModifyTimeAppService(IExamModifyTimeRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override  IQueryable<ExamModifyTime> CreateFilteredQuery(ExamModifyTimeGetListInput input)
    {
        // TODO: AbpHelper generated
        return base.CreateFilteredQuery(input)
            .WhereIf(input.ExamId != null, x => x.ExamId == input.ExamId)
            .WhereIf(input.DelayTime != null, x => x.DelayTime == input.DelayTime)
            .WhereIf(input.ExtendTime != null, x => x.ExtendTime == input.ExtendTime)
            .WhereIf(input.OrganizationUnitId != null, x => x.OrganizationUnitId == input.OrganizationUnitId)
            .WhereIf(input.ExamineeId != null, x => x.ExamineeId == input.ExamineeId)
            ;
    }
}
