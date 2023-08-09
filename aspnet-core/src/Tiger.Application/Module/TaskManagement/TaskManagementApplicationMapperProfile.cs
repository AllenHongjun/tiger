using AutoMapper;
using Tiger.Module.TaskManagement.Dtos;

namespace Tiger.Module.TaskManagement;

public class TaskManagementApplicationMapperProfile : Profile
{
    public TaskManagementApplicationMapperProfile()
    {
        CreateMap<BackgroundJobInfo, BackgroundJobInfoDto>();
        CreateMap<BackgroundJobLog, BackgroundJobLogDto>();
        CreateMap<BackgroundJobAction, BackgroundJobActionDto>();
    }
}
