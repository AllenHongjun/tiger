using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.System.Platform.Datas.Dtos;
using Tiger.Module.System.Platform.Datas;
using Tiger.Module.Schools.Dtos;

namespace Tiger.Module.Schools
{
    public class SchoolApplicationMappingProfile:Profile
    {
        public SchoolApplicationMappingProfile()
        {
            CreateMap<School, SchoolDto>();
            CreateMap<CreateUpdateSchoolDto, School>();
        }
    }
}
