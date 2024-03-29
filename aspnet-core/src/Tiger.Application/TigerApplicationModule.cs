﻿using Microsoft.Extensions.DependencyInjection;
using Tiger.Blob.Qinui;
using Tiger.Infrastructure.Notification.Emailing;
using Tiger.Infrastructure.Notification;
using Tiger.Module.OssManagement;
using Tiger.Module.OssManagement.Aliyun;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs.Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.Caching;
using Volo.Abp.EventBus;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Sms;
using Volo.Abp.TenantManagement;
using Volo.Abp.VirtualFileSystem;
using Tiger.Infrastructure.Notification.SignalR;
using Volo.Abp.AspNetCore.SignalR;

namespace Tiger
{
    /// <summary>
    /// 项目包含 .Application.Contracts 项目的 应用服务 接口实现.
    /// 它依赖 .Application.Contracts 项目, 因为它需要实现接口与使用DTO.
    /// 它依赖 .Domain 项目,因为它需要使用领域对象(实体,仓储接口等)执行应用程序逻辑.
    /// </summary>
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
        typeof(AbpBackgroundWorkersQuartzModule),
        typeof(AbpBackgroundJobsQuartzModule), //Add the new module dependency Quartz
        typeof(AbpSmsModule), //Add the new module dependency
        typeof(AbpCachingModule), // 缓存
      //typeof(AbpBackgroundJobsModule), // 后台任务  默认后台作业管理器
        typeof(AbpEventBusModule),
        typeof(AbpAspNetCoreSignalRModule) //Add the new module dependency
        )]
    public class TigerApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            #region 配置虚拟路径
            Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.AddEmbedded<TigerApplicationModule>();
                }); 
            #endregion

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
            Configure<AbpBackgroundJobQuartzOptions>(options =>
                {
                    // 每次间隔5秒 重试3次
                    options.RetryCount = 3;
                    options.RetryIntervalMillisecond = 5000;

                    //选项自定义异常处理策略:
                    //options.RetryStrategy = async (retryIndex, executionContext, exception) =>
                    //{
                    //    // customize exception handling
                    //};
                });
            #endregion

            #region 阿里云容器实现注入
            // 在模块中依赖注入阿里云容器的实现（这个不能命名的类不知自动注入）
            context.Services.AddTransient<IOssContainerFactory, AliyunOssContainerFactory>();
            #endregion

            #region StackExchangeRedis缓存
            // 注入StackExchangeRedis缓存管理实现
            //context.Services.AddSingleton<ICacheManager, StackExchangeRedisCacheManager>(); 
            #endregion

            #region 通知提供者注入
            Configure<AbpNotificationsPublishOptions>(options =>
                {
                    options.PublishProviders.Add<EmailingNotificationPublishProvider>();
                    options.NotificationDataMappings
                           .MappingDefault(EmailingNotificationPublishProvider.ProviderName, data => data);
                });

            //var preSmsActions = context.Services.GetPreConfigureActions<AbpNotificationsSmsOptions>();
            //Configure<AbpNotificationsSmsOptions>(options =>
            //{
            //    preSmsActions.Configure(options);
            //});

            //Configure<AbpNotificationsPublishOptions>(options =>
            //{
            //    options.PublishProviders.Add<SmsNotificationPublishProvider>();

            //    var smsOptions = preSmsActions.Configure(options);

            //    options.NotificationDataMappings
            //           .MappingDefault(
            //                SmsNotificationPublishProvider.ProviderName,
            //                data => NotificationData.ToStandardData(smsOptions.TemplateParamsPrefix, data));
            //});


            Configure<AbpNotificationsPublishOptions>(options =>
            {
                options.PublishProviders.Add<SignalRNotificationPublishProvider>();
                options.NotificationDataMappings
                       .MappingDefault(SignalRNotificationPublishProvider.ProviderName,
                       data => data.ToSignalRData());
            });
            #endregion


        }




    }
}
