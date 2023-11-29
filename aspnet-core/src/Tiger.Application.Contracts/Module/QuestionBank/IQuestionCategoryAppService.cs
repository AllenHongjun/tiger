using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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
    Task<IActionResult> ExportToXlsxAsync(QuestionCategoryGetListInput input);
    ListResultDto<QuestionCategoryDto> GetAllList(QuestionCategoryGetListInput input);
    ListResultDto<QuestionCategoryDto> GetListByParentId(QuestionCategoryGetListInput input);
    Task ImportFromXlsxAsync(IFormFile importexcelfile);
}