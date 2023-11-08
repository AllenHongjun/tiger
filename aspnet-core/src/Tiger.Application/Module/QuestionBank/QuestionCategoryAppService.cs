using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.QuestionBank.Dtos;
using Volo.Abp.Application.Services;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Volo.Abp.Application.Dtos;
using Tiger.Module.System.Platform.Layouts.Dto;
using System.Collections.Generic;
using Qiniu.Storage;
using System.Linq.Dynamic.Core;
using Volo.Abp;

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


    public  ListResultDto<QuestionCategoryDto> GetListByParentId(Guid? parentId)
    {
        var questionCategories = _repository.WhereIf(parentId !=null, x=> x.ParentId == parentId)
                .ToList();
        return new ListResultDto<QuestionCategoryDto>(
            ObjectMapper.Map<List<QuestionCategory>, List<QuestionCategoryDto>>(questionCategories));

    }


    public ListResultDto<QuestionCategoryDto> GetAllList(QuestionCategoryGetListInput input)
    {
        var questionCategories = CreateFilteredQuery(input).ToList();
        return new ListResultDto<QuestionCategoryDto>(
            ObjectMapper.Map<List<QuestionCategory>, List<QuestionCategoryDto>>(questionCategories));
    }

    public override async Task<QuestionCategoryDto> CreateAsync(CreateUpdateQuestionCategoryDto input)
    {
        var questionCategory =  _repository.Where(x => x.Name == input.Name).FirstOrDefault();
        if (questionCategory != null)
        {
            throw new UserFriendlyException(L["DuplicateQuestionCategory", input.Name]);
        }

        return await base.CreateAsync(input);
    }

    public override async Task<QuestionCategoryDto> UpdateAsync(Guid id, CreateUpdateQuestionCategoryDto input)
    {
        var questionCategory = _repository.Where(x => x.Name == input.Name).FirstOrDefault();
        if (questionCategory != null)
        {
            throw new UserFriendlyException(L["DuplicateQuestionCategory", input.Name]);
        }

        return await base.UpdateAsync(id, input); 
    }
}
