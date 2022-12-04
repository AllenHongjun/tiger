using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Volo.Abp.IdentityServer.ApiResources.Dto;
using Tiger.Volo.Abp.IdentityServer.Clients.Dto;
using Tiger.Volo.Abp.IdentityServer.Devices;
using Tiger.Volo.Abp.IdentityServer.Grants;
using Tiger.Volo.Abp.IdentityServer.IdentityResources.Dto;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.Devices;
using Volo.Abp.IdentityServer.Grants;
using Volo.Abp.IdentityServer.IdentityResources;

namespace Tiger.Volo.Abp.IdentityServer
{
    public class AbpIdentityServerModuleAutoMapperProfile : Profile
    {
        public AbpIdentityServerModuleAutoMapperProfile()
        {
            CreateMap<ApiResource, ApiResourceDto>();
            CreateMap<CreateUpdateApiResourceDto, ApiResource>();

            CreateMap<Client, ClientDto>();
            CreateMap<CreateUpdateClientDto, ClientDto>();

            CreateMap<DeviceFlowCodes, DeviceFlowCodeDto>();
            CreateMap<CreateUpdateDeviceFlowCodeDto, DeviceFlowCodeDto>();

            CreateMap<PersistedGrant, PersistedGrantDto>();
            CreateMap<CreateUpdatePersistedGrantDto, PersistedGrant>();

            CreateMap<IdentityResource, IdentityResourceDto>();
            CreateMap<CreateUpdateIdentityResourceDto, IdentityResource>();
        }
    }
}
