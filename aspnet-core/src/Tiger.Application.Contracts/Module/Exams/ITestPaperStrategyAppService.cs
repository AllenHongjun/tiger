using System;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Exams;


/// <summary>
/// 组卷策略配置表
/// </summary>
public interface ITestPaperStrategyAppService :
    ICrudAppService< 
                TestPaperStrategyDto, 
        Guid, 
        TestPaperStrategyGetListInput,
        CreateUpdateTestPaperStrategyDto,
        CreateUpdateTestPaperStrategyDto>
{

}