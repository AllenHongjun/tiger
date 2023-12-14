using System;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Exams;


/// <summary>
/// 试卷评委表        <remarks>    评卷人只有关联了试卷才能改卷（默认只有超管能改卷）    </remarks>
/// </summary>
public interface ITestPaperJudgeAppService :
    ICrudAppService< 
                TestPaperJudgeDto, 
        Guid, 
        TestPaperJudgeGetListInput,
        CreateUpdateTestPaperJudgeDto,
        CreateUpdateTestPaperJudgeDto>
{

}