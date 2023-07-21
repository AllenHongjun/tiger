using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Books.Demo;
using Tiger.Module.System.Platform.Datas.Dtos;
using Tiger.Module.System.Platform.Datas;
using Tiger.Module.System.Platform.Menus;
using Tiger.Module.System.Platform.Versions;
using Tiger.Module.System.Platform.Menus.Dto;

namespace Tiger.Module.System.Platform
{
    public class PlatformApplicationMappingProfile:Profile
    {
        public PlatformApplicationMappingProfile()
        {
            //CreateMap<VersionFile, VersionFileDto>();
            //CreateMap<AppVersion, VersionDto>();

            CreateMap<DataItem, DataItemDto>();
            CreateMap<Data, DataDto>();
            CreateMap<Menu, MenuDto>()
                .ForMember(dto => dto.Meta, map => map.MapFrom(src => src.ExtraProperties))
                .ForMember(dto => dto.Startup, map => map.Ignore());
            //CreateMap<Layout, LayoutDto>()
            //    .ForMember(dto => dto.Meta, map => map.MapFrom(src => src.ExtraProperties));
            //CreateMap<UserFavoriteMenu, UserFavoriteMenuDto>();
        }

    }
}
