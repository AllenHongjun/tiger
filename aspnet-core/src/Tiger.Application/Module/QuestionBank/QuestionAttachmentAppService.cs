using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.QuestionBank.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;

namespace Tiger.Module.QuestionBank;


/// <summary>
/// 题目附件表
/// </summary>
[RemoteService(false)]
public class QuestionAttachmentAppService : CrudAppService<QuestionAttachment, QuestionAttachmentDto, Guid, QuestionAttachmentGetListInput, CreateUpdateQuestionAttachmentDto, CreateUpdateQuestionAttachmentDto>,
    IQuestionAttachmentAppService
{
    protected override string GetPolicyName { get; set; } = TigerPermissions.QuestionAttachment.Default;
    protected override string GetListPolicyName { get; set; } = TigerPermissions.QuestionAttachment.Default;
    protected override string CreatePolicyName { get; set; } = TigerPermissions.QuestionAttachment.Create;
    protected override string UpdatePolicyName { get; set; } = TigerPermissions.QuestionAttachment.Update;
    protected override string DeletePolicyName { get; set; } = TigerPermissions.QuestionAttachment.Delete;

    private readonly IQuestionAttachmentRepository _repository;

    public QuestionAttachmentAppService(IQuestionAttachmentRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override IQueryable<QuestionAttachment> CreateFilteredQuery(QuestionAttachmentGetListInput input)
    {
        // TODO: AbpHelper generated
        return base.CreateFilteredQuery(input)
            .WhereIf(input.QuestionId != null, x => x.QuestionId == input.QuestionId)
            .WhereIf(input.AttachmentType != null, x => x.AttachmentType == input.AttachmentType)
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(!input.Content.IsNullOrWhiteSpace(), x => x.Content.Contains(input.Content))
            .WhereIf(input.Sorting != null, x => x.Sorting == input.Sorting)
            ;
    }
}
