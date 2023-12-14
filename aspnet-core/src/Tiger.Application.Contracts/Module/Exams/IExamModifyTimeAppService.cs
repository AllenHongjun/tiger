using System;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Exams;


/// <summary>
/// 考试时间调整表
/// </summary>
public interface IExamModifyTimeAppService :
    ICrudAppService< 
                ExamModifyTimeDto, 
        Guid, 
        ExamModifyTimeGetListInput,
        CreateUpdateExamModifyTimeDto,
        CreateUpdateExamModifyTimeDto>
{

}