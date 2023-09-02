using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tiger.MultiTenancy;
using Tiger.Volo.Abp.Sass.Editions;
using Tiger.Volo.Abp.Sass;
using Volo.Abp.AuditLogging;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundJobs.Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Emailing;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.IdentityServer;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Tiger.Module.OssManagement.Aliyun;
using Tiger.Module.OssManagement;
using Tiger.Module.OssManagement.Localization;
using Tiger.Localization;
using Tiger.Module.System.Platform.Localization;
using Tiger.Module.System.TextTemplate.Localization;
using Tiger.Volo.Abp.SettingUi.Localization;
using Volo.Abp.AuditLogging.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Localization;
using Volo.Abp.SettingManagement.Localization;
using Volo.Abp.Validation.Localization;
using Tiger.Infrastructure.CloudAliyun.Aliyun.Localization;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.BlobStoring;
using Tiger.Infrastructure.CloudAliyun.BlobStoring.Aliyun;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Quartz;
using Tiger.Infrastructure.BackgroundTasks.Quartz;
using Volo.Abp;
using Volo.Abp.Quartz;
using Tiger.Infrastructure.BackgroundTasks;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;
using Tiger.Infrastructure.BackgroundTasks.Internal;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Guids;
using Volo.Abp.EventBus;
using Tiger.Module.TaskManagement;
using Volo.Abp.Sms;

namespace Tiger
{
    /// <summary>
    /// 领域模块
    /// </summary>
    /// <remarks>
    /// 解决方案的领域层. 它主要包含 实体, 集合根, 领域服务, 值类型, 仓储接口 和解决方案的其他领域对象.
    /// </remarks>
    [DependsOn(
        typeof(TigerDomainSharedModule),
        typeof(AbpAuditLoggingDomainModule),
        typeof(AbpBackgroundJobsDomainModule),
        typeof(AbpBackgroundJobsQuartzModule),
        typeof(AbpBackgroundWorkersQuartzModule),
        typeof(AbpQuartzModule),
        typeof(AbpFeatureManagementDomainModule),
        typeof(AbpIdentityDomainModule),
        typeof(AbpPermissionManagementDomainIdentityModule),
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpPermissionManagementDomainIdentityServerModule),
        typeof(AbpSettingManagementDomainModule),
        typeof(AbpTenantManagementDomainModule),
        typeof(AbpEmailingModule)

    )]

    [DependsOn(typeof(AbpBackgroundJobsAbstractionsModule))] // AbpBackgroundJobsAbstractions 定时任务需要集成 abp后台作业模块
    [DependsOn(typeof(AbpEventBusModule))]
    [DependsOn(typeof(AbpBackgroundWorkersModule))]
    [DependsOn(typeof(AbpGuidsModule))]
    public class TigerDomainModule : AbpModule
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            #region Abp.BackgroundTasks.Quartz模块注入

            var _scheduler = context.ServiceProvider.GetRequiredService<IScheduler>();
            _scheduler.ListenerManager.AddJobListener(context.ServiceProvider.GetRequiredService<QuartzJobListener>());
            _scheduler.ListenerManager.AddTriggerListener(context.ServiceProvider.GetRequiredService<QuartzTriggerListener>());


            #endregion
        }

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AutoAddDefinitionProviders(context.Services);
        }

        #region 任务定义注入
        private static void AutoAddDefinitionProviders(IServiceCollection services)
        {
            var definitionProviders = new List<Type>();

            services.OnRegistred(context =>
            {
                if (typeof(IJobDefinitionProvider).IsAssignableFrom(context.ImplementationType))
                {
                    definitionProviders.Add(context.ImplementationType);
                }
            });

            services.Configure<AbpBackgroundTasksOptions>(options =>
            {
                options.DefinitionProviders.AddIfNotContains(definitionProviders);
            });
        }
        #endregion

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<TigerDomainModule>();

            #region BackgroundWorkers初始化（类注入）
            context.Services.AddTransient(typeof(BackgroundJobAdapter<>));
            context.Services.AddSingleton(typeof(BackgroundWorkerAdapter<>));

            // 尝试依赖注入需要类
            context.Services.AddTransient(typeof(BackgroundWorkerAdapter<>));
            context.Services.AddTransient(typeof(QuartzJobSearchJobAdapter));
            context.Services.AddTransient(typeof(QuartzJobSimpleAdapter<>));

            context.Services.AddHostedService<DefaultBackgroundWorker>();

            Configure<AbpBackgroundTasksOptions>(options =>
            {
                options.JobMonitors.AddIfNotContains(typeof(JobExecutedEvent));
                options.JobMonitors.AddIfNotContains(typeof(JobLogEvent));
            });


            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<TaskManagementDomainMapperProfile>(validate: true);
            });

            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.EtoMappings.Add<Module.TaskManagement.BackgroundJobInfo, BackgroundJobEto>();

                // 决定是否应该针对给定的实体类型发布事件. 
                options.AutoEventSelectors.Add<Module.TaskManagement.BackgroundJobInfo>();
            });
            #endregion


            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });
            

            #region Saas

            // 配置Domain模块使用automapper
            Configure<AbpAutoMapperOptions>(options =>
                {
                    options.AddProfile<AbpSaasDomainMappingProfile>(validate: true);
                });

            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.EtoMappings.Add<Edition, EditionEto>();
                options.EtoMappings.Add<Tenant, TenantEto>();

                options.AutoEventSelectors.Add<Edition>();
                options.AutoEventSelectors.Add<Tenant>();
            });
            #endregion

            #region Rescource 资源配置
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                // "TigerDomainSharedModule" 是项目的根命名空间名字. 如果你的项目的根命名空间名字为空,则无需传递此参数.
                options.FileSets.AddEmbedded<TigerDomainModule>();
            });

            // 定义新的资源类 需要在模块中引入配置
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    // 添加了一个新的本地化资源, 使用"zh-Hans"（英语）作为默认的本地化.
                    .Add<AliyunResource>("zh-Hans")
                    .AddVirtualJson("/Infrastructure/CloudAliyun/Aliyun/Localization/Resources");// 用JSON文件存储本地化字符串. 使用虚拟文件系统 将JSON文件嵌入到程序集中.
                //options.DefaultResourceType = typeof(AliyunResource);
            });


            #endregion

            #region 读取阿里云oss对象存储配置 按需引用
            var configuration = context.Services.GetConfiguration();

            Configure<AbpBlobStoringOptions>(options =>
            {
                context.Services.ExecutePreConfiguredActions(options);
                options.Containers.ConfigureAll((containerName, containerConfiguration) =>
                {
                    containerConfiguration.UseAliyun(aliyun =>
                    {
                        aliyun.BucketName = configuration[AliyunBlobProviderConfigurationNames.BucketName] ?? "";
                        aliyun.CreateBucketIfNotExists = configuration.GetSection(AliyunBlobProviderConfigurationNames.CreateBucketIfNotExists).Get<bool>();
                        aliyun.CreateBucketReferer = configuration.GetSection(AliyunBlobProviderConfigurationNames.CreateBucketReferer).Get<List<string>>();
                        aliyun.Endpoint = configuration[AliyunBlobProviderConfigurationNames.Endpoint];
                    });
                });
            });
            #endregion

#if DEBUG
            //NullEmailSender 是实现 IEmailSender 的内置类，但将电子邮件内容写入 标准日志系统，而不是实际发送电子邮件。
            //context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
            //context.Services.Replace(ServiceDescriptor.Singleton<ISmsSender, NullSmsSender>());
#endif
        }



    }
}
