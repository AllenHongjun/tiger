using System;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Exams;


/// <summary>
/// 答卷表
/// </summary>
public interface IAnswerSheetAppService :
    ICrudAppService<
                AnswerSheetDto,
        Guid,
        AnswerSheetGetListInput,
        CreateUpdateAnswerSheetDto,
        CreateUpdateAnswerSheetDto>
{
    ExamScorePanelDto GetExamScorePanelData(Guid examId);
}