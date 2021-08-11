using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.Caching;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Sms;
using Volo.Abp.TenantManagement;

namespace TigerAdmin
{
    [DependsOn(
        typeof(TigerAdminDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(TigerAdminApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpBlobStoringModule),
        typeof(AbpBlobStoringFileSystemModule),
        typeof(AbpSmsModule), //Add the new module dependency
        typeof(AbpCachingModule)

        )]
    public class TigerAdminApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<TigerAdminApplicationModule>();
            });


            // 配置使用对象存储
            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.ConfigureDefault(container =>
                {
                    container.UseFileSystem(fileSystem =>
                    {
                        fileSystem.BasePath = "D:\\tiger-files";
                    });
                    //TODO...
                    container.IsMultiTenant = true;
                });
            });

            context.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "127.0.0.1:6379,ConnectTimeout=15000,SyncTimeout=5000";
                //options.InstanceName
                //options.ConfigurationOptions
            });
        }
    }
}
