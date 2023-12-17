using System;
using System.Threading.Tasks;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Exams;


/// <summary>
/// 考试人员表(应试人；参加考试者)
/// </summary>
public interface IExamineeAppService :
    ICrudAppService<
                ExamineeDto,
        Guid,
        ExamineeGetListInput,
        CreateUpdateExamineeDto,
        CreateUpdateExamineeDto>
{
    Task BulkCreateAsync(ExamineeBatchInputDto input);
    Task BulkDeleteAsync(ExamineeBatchInputDto input);
}