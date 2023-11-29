using System;
using System.Threading.Tasks;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Dtos;
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
    Task<ListResultDto<TestPaperDto>> GetAllAsync();
}