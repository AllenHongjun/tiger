using Tiger.Module.TaskManagement.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.TaskManagement;

public interface IBackgroundJobLogAppService :
    IReadOnlyAppService<
        BackgroundJobLogDto,
        long,
        BackgroundJobLogGetListInput>,
    IDeleteAppService<long>
{
}
