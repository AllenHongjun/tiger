using System;
using Tiger.Module.QuestionBank.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.QuestionBank;


/// <summary>
/// 题目分类
/// </summary>
public interface IQuestionCategoryAppService :
    ICrudAppService<
                QuestionCategoryDto,
        Guid,
        QuestionCategoryGetListInput,
        CreateUpdateQuestionCategoryDto,
        CreateUpdateQuestionCategoryDto>
{
    ListResultDto<QuestionCategoryDto> GetAllList(QuestionCategoryGetListInput input);
    ListResultDto<QuestionCategoryDto> GetListByParentId(QuestionCategoryGetListInput input);
}