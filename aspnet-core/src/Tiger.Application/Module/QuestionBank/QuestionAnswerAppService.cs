using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.QuestionBank.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.QuestionBank;


/// <summary>
/// 题目答案
/// </summary>
public class QuestionAnswerAppService : CrudAppService<QuestionAnswer, QuestionAnswerDto, Guid, QuestionAnswerGetListInput, CreateUpdateQuestionAnswerDto, CreateUpdateQuestionAnswerDto>,
    IQuestionAnswerAppService
{
    protected override string GetPolicyName { get; set; } = TigerPermissions.QuestionAnswer.Default;
    protected override string GetListPolicyName { get; set; } = TigerPermissions.QuestionAnswer.Default;
    protected override string CreatePolicyName { get; set; } = TigerPermissions.QuestionAnswer.Create;
    protected override string UpdatePolicyName { get; set; } = TigerPermissions.QuestionAnswer.Update;
    protected override string DeletePolicyName { get; set; } = TigerPermissions.QuestionAnswer.Delete;

    private readonly IQuestionAnswerRepository _repository;

    public QuestionAnswerAppService(IQuestionAnswerRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override  IQueryable<QuestionAnswer> CreateFilteredQuery(QuestionAnswerGetListInput input)
    {
        // TODO: AbpHelper generated
        return base.CreateFilteredQuery(input)
            .WhereIf(input.QuestionId != null, x => x.QuestionId == input.QuestionId)
            .WhereIf(!input.Answer.IsNullOrWhiteSpace(), x => x.Answer.Contains(input.Answer))
            ;
    }
}
