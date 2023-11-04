using AutoMapper;
using Tiger.Module.Schools.Dtos;

namespace Tiger.Module.Schools
{
    public class SchoolApplicationMappingProfile:Profile
    {
        public SchoolApplicationMappingProfile()
        {
            CreateMap<School, SchoolDto>();
            CreateMap<CreateUpdateSchoolDto, School>();

            CreateMap<ClassInfo, ClassInfoDto>();
            CreateMap<CreateUpdateClassInfoDto, ClassInfo>();

        }
    }
}
