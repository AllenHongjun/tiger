using AutoMapper;
using Tiger.Volo.Abp.Sass.Editions;
using Tiger.Volo.Abp.Sass.Tenants;

namespace Tiger.Volo.Abp.Sass;

public class AbpSaasApplicationAutoMapperProfile : Profile
{
    public AbpSaasApplicationAutoMapperProfile()
    {
        CreateMap<TenantConnectionString, TenantConnectionStringDto>();

        CreateMap<Tenant, TenantDto>()
            .ForMember(dto => dto.EditionId, map =>
            {
                map.MapFrom((tenant, dto) =>
                {
                    return tenant.Edition?.Id;
                });
            })
            // 实体类增加显示的字段，赋值的时候将字段赋值带上,将字段不映射到数据库中
            .ForMember(dto => dto.EditionName, map =>
            {
                map.MapFrom((tenant, dto) =>
                {
                    return tenant.Edition?.DisplayName;
                });
            })
            .MapExtraProperties();

        CreateMap<Edition, EditionDto>()
            .MapExtraProperties();
    }
}
