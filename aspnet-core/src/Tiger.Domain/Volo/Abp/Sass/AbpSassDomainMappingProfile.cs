using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Volo.Abp.Sass.Editions;
using Tiger.Volo.Abp.Sass.Tenants;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace Tiger.Volo.Abp.Sass
{   
    /// <summary>
    /// Sass领域层对象映射
    /// </summary>
    public class AbpSassDomainMappingProfile : Profile
    {
        public AbpSassDomainMappingProfile()
        {
            //CreateMap<Edition, EditionInfo>();
            CreateMap<Edition, EditionEto>();

            //CreateMap<Tenant, TenantConfiguration>()
            //    .ForMember(ti => ti.ConnectionStrings, opts =>
            //    {
            //        opts.MapFrom((tenant, ti) =>
            //        {
            //            var connStrings = new ConnectionStrings();

            //            foreach (var connectionString in tenant.ConnectionStrings)
            //            {
            //                connStrings[connectionString.Name] = connectionString.Value;
            //            }

            //            return connStrings;
            //        });
            //    })
            //    .ForMember(ti => ti.IsActive, opts =>
            //    {
            //        opts.MapFrom((tenant, ti) =>
            //        {
            //            if (!tenant.IsActive)
            //            {
            //                return false;
            //            }
            //            // Injection IClock ?
            //            if (tenant.EnableTime.HasValue && DateTime.Now < tenant.EnableTime)
            //            {
            //                return false;
            //            }

            //            if (tenant.DisableTime.HasValue && DateTime.Now > tenant.DisableTime)
            //            {
            //                return false;
            //            }

            //            return true;
            //        });
            //    });

            CreateMap<Tenant, TenantEto>();
        }
    }
}
