using System;
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

}