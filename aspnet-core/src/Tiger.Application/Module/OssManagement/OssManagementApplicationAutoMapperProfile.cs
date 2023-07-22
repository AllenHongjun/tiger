using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.OssManagement.Dto;
using Tiger.Module.OssManagement.Dtos;

namespace Tiger.Module.OssManagement
{
    public class OssManagementApplicationAutoMapperProfile : Profile
    {
        public OssManagementApplicationAutoMapperProfile()
        {
            CreateMap<OssContainer, OssContainerDto>();
            CreateMap<OssObject, OssObjectDto>()
                .ForMember(dto => dto.Path, map => map.MapFrom(src => src.Prefix));

            CreateMap<GetOssContainersResponse, OssContainersResultDto>();
            CreateMap<GetOssObjectsResponse, OssObjectsResultDto>();

            CreateMap<FileShareCacheItem, MyFileShareDto>();
        }
    }
}
