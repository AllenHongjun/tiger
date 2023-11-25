using System;
using System.Threading.Tasks;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Dtos;
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
    ListResultDto<TestPaperSectionDto> GetAll(TestPaperSectionGetListInput input);
    Task MoveDownAsync(Guid id);
}