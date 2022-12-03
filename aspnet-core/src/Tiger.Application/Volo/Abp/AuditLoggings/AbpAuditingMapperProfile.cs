using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Volo.Abp.AuditLogging.Dto;
using Volo.Abp.AuditLogging;

namespace Tiger.Volo.Abp.AuditLoggings
{
    public class AbpAuditingMapperProfile : Profile
    {
        public AbpAuditingMapperProfile()
        {
            CreateMap<EntityChangeWithUsername, EntityChangeWithUsernameDto>();
        }
    }
}
