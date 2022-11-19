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
        typeof(AbpCachingModule), // 缓存
        typeof(AbpBackgroundJobsModule), // 后台任务
        //typeof(AbpBackgroundJobsQuartzModule), //Add the new module dependency Quartz
        typeof(AbpBackgroundWorkersQuartzModule) //Quartz 定时任务(abp叫后台工作者)
        )]
    public class TigerApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                //Add all mappings defined in the assembly of the TigerApplicationModule class
                options.AddMaps<TigerApplicationModule>();

                //AddMaps 使用可选的 bool 参数控制模块的配置验证:
                //options.AddMaps<TigerApplicationModule>(validate: true);

            });


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



            //// 配置模块使用redis缓存
            //context.Services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = "127.0.0.1:6379,ConnectTimeout=15000,SyncTimeout=5000";
            //    //options.InstanceName
            //    //options.ConfigurationOptions
            //});


            
            Configure<AbpBackgroundJobQuartzOptions>(options =>
            {
                // 定时任务重试次数配置
                options.RetryCount = 5;
                options.RetryIntervalMillisecond = 5000;

                //选项自定义异常处理策略:
                //options.RetryStrategy = async (retryIndex, executionContext, exception) =>
                //{
                //    // customize exception handling
                //};
            });



        }


        /// <summary>
        /// 预配置 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {



            //定时任务。
            //var configuration = context.Services.GetConfiguration();

            // 第一种配置方式
            //PreConfigure<AbpQuartzOptions>(options =>
            //{
            //    options.Properties = new NameValueCollection
            //    {
            //        ["quartz.jobStore.dataSource"] = "BackgroundJobsDemoApp",
            //        ["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz",
            //        ["quartz.jobStore.tablePrefix"] = "QRTZ_",
            //        ["quartz.serializer.type"] = "json",
            //        ["quartz.dataSource.BackgroundJobsDemoApp.connectionString"] = configuration.GetConnectionString("Quartz"),
            //        ["quartz.dataSource.BackgroundJobsDemoApp.provider"] = "SqlServer",
            //        ["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz",
            //    };
            //});



            //从ABP3.1版本开始,我们在 AbpQuartzOptions 添加了 Configurator 用于配置Quartz. 例:
            //PreConfigure<AbpQuartzOptions>(options =>
            //{
            //    options.Configurator = configure =>
            //    {
            //        configure.UsePersistentStore(storeOptions =>
            //        {
            //            storeOptions.UseProperties = true;
            //            //storeOptions.UseJsonSerializer();
            //            storeOptions.UseSqlServer(configuration.GetConnectionString("Quartz"));
            //            storeOptions.UseClustering(c =>
            //            {
            //                c.CheckinMisfireThreshold = TimeSpan.FromSeconds(20);
            //                c.CheckinInterval = TimeSpan.FromSeconds(10);
            //            });
            //        });
            //    };
            //});
        }
    }
}
