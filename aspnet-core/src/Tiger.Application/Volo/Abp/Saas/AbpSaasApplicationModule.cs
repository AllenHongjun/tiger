﻿using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Tiger.Volo.Abp.Sass;

//[DependsOn(typeof(AbpSaasDomainModule))]
//[DependsOn(typeof(AbpSaasApplicationContractsModule))]
//public class AbpSaasApplicationModule : AbpModule
//{
//    public override void ConfigureServices(ServiceConfigurationContext context)
//    {
//        context.Services.AddAutoMapperObjectMapper<AbpSaasApplicationModule>();
//        Configure<AbpAutoMapperOptions>(options =>
//        {
//            options.AddProfile<AbpSaasApplicationAutoMapperProfile>(validate: true);
//        });
//    }
//}
