﻿using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Specialized;
using Tiger.Blob.Qinui;
using Tiger.BlobDemo;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.BackgroundJobs.Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.Caching;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Quartz;
using Volo.Abp.Sms;
using Volo.Abp.TenantManagement;

namespace Tiger
{
    [DependsOn(
        typeof(TigerDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(TigerApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpBlobStoringModule),
        typeof(AbpBlobStoringFileSystemModule),
        typeof(AbpSmsModule), //Add the new module dependency
        typeof(AbpCachingModule) // 缓存
                                  //typeof(AbpBackgroundJobsModule), // 后台任务  默认后台作业管理器
        //typeof(AbpBackgroundJobsQuartzModule), //Add the new module dependency Quartz

        
        )]
    public class TigerApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            #region 配置使用 AutoMapper
            Configure<AbpAutoMapperOptions>(options =>
                {
                    //添加在TigerApplicationModule类中定义的所有配置规则
                    options.AddMaps<TigerApplicationModule>();

                    //AddMaps 使用可选的 bool 参数控制模块的配置验证:
                    //options.AddMaps<TigerApplicationModule>(validate: true);

                }); 
            #endregion

            #region 配置使用BLOB
            // 配置使用BLOB
            Configure<AbpBlobStoringOptions>(options =>
            {
                ////配置 demo ProfilePictureContainer
                //options.Containers.Configure<ProfilePictureContainer>(container =>
                //{
                //    //TODO... 在这里配置具体项目


                //    //是否启用多租户
                //    container.IsMultiTenant = true;
                //});

                // 配置使用 QiniuOSS对象存储
                options.Containers.ConfigureDefault(container =>
                {
                    container.ProviderType = typeof(QiniuBlobProvider);
                });

                //// demo配置使用自定义的blob 提供程序
                //options.Containers.ConfigureDefault(container =>
                //{
                //    container.UseMyCustomBlobProvider(provider =>
                //    {
                //        provider.MyOption1 = "my value";
                //    });
                //});
            });
            #endregion

            #region 配置使用redis
            //// 配置模块使用redis缓存
            //context.Services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = "127.0.0.1:6379,ConnectTimeout=15000,SyncTimeout=5000";
            //    //options.InstanceName
            //    //options.ConfigurationOptions
            //}); 
            #endregion

            #region 配置使用AbpQuartz 后台作业
            //Configure<AbpBackgroundJobQuartzOptions>(options =>
            //    {
            //        // 定时任务重试次数配置
            //        options.RetryCount = 5;
            //        options.RetryIntervalMillisecond = 5000;

            //        //选项自定义异常处理策略:
            //        //options.RetryStrategy = async (retryIndex, executionContext, exception) =>
            //        //{
            //        //    // customize exception handling
            //        //};
            //    });
            #endregion

            
        }


        
        
    }
}
