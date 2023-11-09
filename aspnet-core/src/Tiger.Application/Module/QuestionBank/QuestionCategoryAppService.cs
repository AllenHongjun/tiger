using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Tiger.Module.QuestionBank.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.QuestionBank;


/// <summary>
/// 题目分类
/// </summary>
[RemoteService(false)]
public class QuestionCategoryAppService : CrudAppService<QuestionCategory, QuestionCategoryDto, Guid, QuestionCategoryGetListInput, CreateUpdateQuestionCategoryDto, CreateUpdateQuestionCategoryDto>,
    IQuestionCategoryAppService
{

    private readonly IQuestionCategoryRepository _repository;

    public QuestionCategoryAppService(IQuestionCategoryRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override  IQueryable<QuestionCategory> CreateFilteredQuery(QuestionCategoryGetListInput input)
    {
        // TODO: AbpHelper generated
        return  base.CreateFilteredQuery(input)
            .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Filter))
            .WhereIf(input.ParentId != null, x => x.ParentId == input.ParentId)
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(!input.Cover.IsNullOrWhiteSpace(), x => x.Cover.Contains(input.Cover))
            .WhereIf(!input.Code.IsNullOrWhiteSpace(), x => x.Code.Contains(input.Code))
            .WhereIf(input.Enable != null, x => x.Enable == input.Enable)
            .WhereIf(input.IsPublic != null, x => x.IsPublic == input.IsPublic)
            ;
    }


    public  ListResultDto<QuestionCategoryDto> GetListByParentId(QuestionCategoryGetListInput input)
    {
        var questionCategories = _repository.WhereIf(input.ParentId !=null, x=> x.ParentId == input.ParentId)
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
        var pid = input.ParentId;
        if (questionCategory != null)
        {
            throw new UserFriendlyException(L["DuplicateQuestionCategory", input.Name]);
        }

        return await base.CreateAsync(input);
    }

    public override async Task<QuestionCategoryDto> UpdateAsync(Guid id, CreateUpdateQuestionCategoryDto input)
    {
        return await base.UpdateAsync(id, input); 
    }
}
