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
        typeof(AbpFeatureManagementDomainModule),
        typeof(AbpIdentityDomainModule),
        typeof(AbpPermissionManagementDomainIdentityModule),
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpPermissionManagementDomainIdentityServerModule),
        typeof(AbpSettingManagementDomainModule),
        typeof(AbpTenantManagementDomainModule),
        typeof(AbpEmailingModule)

    )]
    public class TigerDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });
            context.Services.AddAutoMapperObjectMapper<TigerDomainModule>();

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

            

#if DEBUG
            //NullEmailSender 是实现 IEmailSender 的内置类，但将电子邮件内容写入 标准日志系统，而不是实际发送电子邮件。
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
    }
}
}
