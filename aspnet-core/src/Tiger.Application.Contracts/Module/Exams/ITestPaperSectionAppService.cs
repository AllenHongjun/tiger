using System;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Exams;


/// <summary>
/// 试卷大题
/// </summary>
public interface ITestPaperSectionAppService :
    ICrudAppService< 
                TestPaperSectionDto, 
        Guid, 
        TestPaperSectionGetListInput,
        CreateUpdateTestPaperSectionDto,
        CreateUpdateTestPaperSectionDto>
{

}