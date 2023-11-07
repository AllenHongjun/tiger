using System;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Exams;


/// <summary>
/// 考试
/// </summary>
public interface IExamAppService :
    ICrudAppService< 
                ExamDto, 
        Guid, 
        ExamGetListInput,
        CreateUpdateExamDto,
        CreateUpdateExamDto>
{

}