using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Module.QuestionBank.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.QuestionBank;


/// <summary>
/// 题目表
/// </summary>
public interface IQuestionAppService :
    ICrudAppService<
                QuestionDto,
        Guid,
        QuestionGetListInput,
        CreateUpdateQuestionDto,
        CreateUpdateQuestionDto>
{
    Task BulkDeleteAsync(QuestionBatchInput input);
    Task<IActionResult> ExportToXlsxAsync(QuestionGetListInput input);
    Task ImportFromXlsxAsync(IFormFile importexcelfile);
}