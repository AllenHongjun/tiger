using System;
using System.Threading.Tasks;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Exams;


/// <summary>
/// 试卷内容(题目)表
/// </summary>
public interface ITestPaperQuestionAppService :
    ICrudAppService<
                TestPaperQuestionDto,
        Guid,
        TestPaperQuestionGetListInput,
        CreateUpdateTestPaperQuestionDto,
        CreateUpdateTestPaperQuestionDto>
{
    Task ComfirmSelect(TestPaperQuestionComfirmSelectDto input);
    Task<ListResultDto<TestPaperQuestionDto>> GetAllAsync(Guid testPaperSectionId);
    Task RandomSelentQuestions(TestPaperQuestionRandomSelectDto input);
}