using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;

namespace Tiger.Module.Exams;


/// <summary>
/// 答卷明细表
/// </summary>
[RemoteService(IsEnabled = false)]
public class AnswerSheetDetailAppService : CrudAppService<AnswerSheetDetail, AnswerSheetDetailDto, Guid, AnswerSheetDetailGetListInput, CreateUpdateAnswerSheetDetailDto, CreateUpdateAnswerSheetDetailDto>,
    IAnswerSheetDetailAppService
{
    protected override string GetPolicyName { get; set; } = TigerPermissions.AnswerSheetDetail.Default;
    protected override string GetListPolicyName { get; set; } = TigerPermissions.AnswerSheetDetail.Default;
    protected override string CreatePolicyName { get; set; } = TigerPermissions.AnswerSheetDetail.Create;
    protected override string UpdatePolicyName { get; set; } = TigerPermissions.AnswerSheetDetail.Update;
    protected override string DeletePolicyName { get; set; } = TigerPermissions.AnswerSheetDetail.Delete;

    private readonly IAnswerSheetDetailRepository _repository;

    public AnswerSheetDetailAppService(IAnswerSheetDetailRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override  IQueryable<AnswerSheetDetail> CreateFilteredQuery(AnswerSheetDetailGetListInput input)
    {
        // TODO: AbpHelper generated
        return base.CreateFilteredQuery(input)
            .WhereIf(input.AnswerSheetId != null, x => x.AnswerSheetId == input.AnswerSheetId)
            .WhereIf(input.TestPaperId != null, x => x.TestPaperId == input.TestPaperId)
            .WhereIf(input.TestPaperDetailId != null, x => x.TestPaperDetailId == input.TestPaperDetailId)
            .WhereIf(input.QuestionId != null, x => x.QuestionId == input.QuestionId)
            .WhereIf(!input.Answer.IsNullOrWhiteSpace(), x => x.Answer.Contains(input.Answer))
            .WhereIf(input.ObjectiveScore != null, x => x.ObjectiveScore == input.ObjectiveScore)
            .WhereIf(input.OperateAutoScore != null, x => x.OperateAutoScore == input.OperateAutoScore)
            .WhereIf(input.OperateManualScore != null, x => x.OperateManualScore == input.OperateManualScore)
            .WhereIf(!input.OperateId.IsNullOrWhiteSpace(), x => x.OperateId.Contains(input.OperateId))
            .WhereIf(input.SyncTime != null, x => x.SyncTime == input.SyncTime)
            .WhereIf(!input.SyncMessage.IsNullOrWhiteSpace(), x => x.SyncMessage.Contains(input.SyncMessage))
            ;
    }
}
