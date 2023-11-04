using System;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Exams;


/// <summary>
/// 试卷
/// </summary>
public interface ITestPaperAppService :
    ICrudAppService< 
                TestPaperDto, 
        Guid, 
        TestPaperGetListInput,
        CreateUpdateTestPaperDto,
        CreateUpdateTestPaperDto>
{

}