using AutoMapper;
using Tiger.Module.Teachings.Dtos;

namespace Tiger.Module.Teachings
{
    public class CourseApplicationMapperProfile : Profile
    {
        public CourseApplicationMapperProfile()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<CreateUpdateCourseDto, Course>();
        }
    }
}
