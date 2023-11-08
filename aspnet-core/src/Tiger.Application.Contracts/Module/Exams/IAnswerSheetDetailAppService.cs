using System;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Exams;


/// <summary>
/// 答卷明细表
/// </summary>
public interface IAnswerSheetDetailAppService :
    ICrudAppService< 
                AnswerSheetDetailDto, 
        Guid, 
        AnswerSheetDetailGetListInput,
        CreateUpdateAnswerSheetDetailDto,
        CreateUpdateAnswerSheetDetailDto>
{

}