using System;
using Tiger.Module.QuestionBank.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.QuestionBank;


/// <summary>
/// 题目答案
/// </summary>
public interface IQuestionAnswerAppService :
    ICrudAppService< 
                QuestionAnswerDto, 
        Guid, 
        QuestionAnswerGetListInput,
        CreateUpdateQuestionAnswerDto,
        CreateUpdateQuestionAnswerDto>
{

}