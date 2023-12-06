using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiger.Module.Exams.Dtos;
using Tiger.Module.QuestionBank.Dtos;
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
    Task<List<ExamScoreAnalysisDto>> GetExamScoreAnalysisAsync(AnswerSheetGetListInput input);
    ExamScorePanelDto GetExamScorePanelData(Guid examId);
    Task<List<QuestionAnalysisDto>> GetQuestionAnalysisAsync(QuestionGetListInput input);
}