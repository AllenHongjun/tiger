using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.System.Area.Dtos;
using Tiger.Module.System.Localization.Dtos;
using Tiger.Module.System.Platform.Datas.Dtos;

namespace Tiger.Module.System.Area
{
    public class AreaMappingProfile : Profile
    {
        public AreaMappingProfile() 
        {
            CreateMap<Region, RegionDto>();
        }
    }
}
