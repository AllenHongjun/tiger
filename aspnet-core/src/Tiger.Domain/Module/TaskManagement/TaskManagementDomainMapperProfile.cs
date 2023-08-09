using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;

namespace Tiger.Module.TaskManagement
{
    public class TaskManagementDomainMapperProfile : Profile
    {
        public TaskManagementDomainMapperProfile()
        {
            CreateMap<BackgroundJobInfo, JobInfo>();
            CreateMap<BackgroundJobInfo, BackgroundJobEto>();
        }
    }
}
