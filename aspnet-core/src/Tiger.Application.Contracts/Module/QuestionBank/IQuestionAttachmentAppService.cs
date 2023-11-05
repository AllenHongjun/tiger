using System;
using Tiger.Module.QuestionBank.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.QuestionBank;


/// <summary>
/// 题目附件表
/// </summary>
public interface IQuestionAttachmentAppService :
    ICrudAppService< 
                QuestionAttachmentDto, 
        Guid, 
        QuestionAttachmentGetListInput,
        CreateUpdateQuestionAttachmentDto,
        CreateUpdateQuestionAttachmentDto>
{

}