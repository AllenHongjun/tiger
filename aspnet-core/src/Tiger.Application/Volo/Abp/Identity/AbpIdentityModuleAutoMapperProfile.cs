using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Volo.Abp.Identity.ClaimTypes.Dto;
using Tiger.Volo.Abp.Identity.IdentitySecurityLogs.Dto;
using Tiger.Volo.Abp.Identity.OrganizationUnits.Dto;
using Tiger.Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity
{
    public class AbpIdentityModuleAutoMapperProfile : Profile
    {
        public AbpIdentityModuleAutoMapperProfile()
        {
            
            CreateMap<IdentityRoleClaim, IdentityClaimDto>();
            CreateMap<IdentityUser, IdentityUserDto>()
                .MapExtraProperties();
            CreateMap<IdentityRole, IdentityRoleDto>()
                .MapExtraProperties();
            CreateMap<OrganizationUnit, OrganizationUnitDto>()
                .MapExtraProperties();
            CreateMap<IdentityClaimType, ClaimTypeDto>()
                .MapExtraProperties();
            CreateMap<IdentityUserClaim, IdentityClaimDto>();
            CreateMap<IdentitySecurityLog, IdentitySecurityLogDto>();
        }
    }
}
