using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.SecurityLogs;
using Tiger.Volo.Abp.AuditLogging.Dto;
using Volo.Abp.AuditLogging;
using Volo.Abp.AutoMapper;
using Volo.Abp.SecurityLog;

namespace Tiger.Volo.Abp.AuditLoggings
{
    public class AbpAuditingMapperProfile : Profile
    {
        public AbpAuditingMapperProfile()
        {
            CreateMap<EntityChangeWithUsername, EntityChangeWithUsernameDto>();

            CreateMap<SecurityLogInfo, SecurityLogDto>();

            CreateMap<AuditLogAction, AuditLogActionDto>();
            CreateMap<AuditLog, AuditLogDto>()

                // 从DTO映射时你可能想忽略这些基本属性
                .IgnoreAuditedObjectProperties()
                .IgnoreFullAuditedObjectProperties()
                .IgnoreCreationAuditedObjectProperties()

                // ABP 中可以这样写来忽略
                .Ignore(x => x.CreationTime)
                // 在AutoMapper中,通常可以编写这样的映射代码来忽略属性
                .ForMember(x => x.CreationTime, map => map.Ignore())
                // MapExtraPropertiesTo 是ABP框架提供的扩展方法,用于以受控方式将额外的属性从一个对象复制到另一个对象
                .MapExtraProperties();

            CreateMap<EntityChange, EntityChangeDto>()
                .MapExtraProperties();
            CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();


        }
    }
}
