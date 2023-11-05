using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.QuestionBank.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.QuestionBank;


/// <summary>
/// 题目分类
/// </summary>
public class QuestionCategoryAppService : CrudAppService<QuestionCategory, QuestionCategoryDto, Guid, QuestionCategoryGetListInput, CreateUpdateQuestionCategoryDto, CreateUpdateQuestionCategoryDto>,
    IQuestionCategoryAppService
{
    protected override string GetPolicyName { get; set; } = TigerPermissions.QuestionCategory.Default;
    protected override string GetListPolicyName { get; set; } = TigerPermissions.QuestionCategory.Default;
    protected override string CreatePolicyName { get; set; } = TigerPermissions.QuestionCategory.Create;
    protected override string UpdatePolicyName { get; set; } = TigerPermissions.QuestionCategory.Update;
    protected override string DeletePolicyName { get; set; } = TigerPermissions.QuestionCategory.Delete;

    private readonly IQuestionCategoryRepository _repository;

    public QuestionCategoryAppService(IQuestionCategoryRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override  IQueryable<QuestionCategory> CreateFilteredQuery(QuestionCategoryGetListInput input)
    {
        // TODO: AbpHelper generated
        return  base.CreateFilteredQuery(input)
            .WhereIf(input.ParentId != null, x => x.ParentId == input.ParentId)
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(!input.Cover.IsNullOrWhiteSpace(), x => x.Cover.Contains(input.Cover))
            .WhereIf(!input.Code.IsNullOrWhiteSpace(), x => x.Code.Contains(input.Code))
            .WhereIf(input.Enable != null, x => x.Enable == input.Enable)
            .WhereIf(input.IsPublic != null, x => x.IsPublic == input.IsPublic)
            ;
    }
}
