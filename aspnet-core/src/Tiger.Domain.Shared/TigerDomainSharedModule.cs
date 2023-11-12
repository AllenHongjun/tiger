using Localization.Resources.AbpUi;
using Tiger.Localization;
using Tiger.Module.Exams.Localization;
using Tiger.Module.OssManagement.Localization;
using Tiger.Module.QuestionBank.Localization;
using Tiger.Module.Schools.Localization;
using Tiger.Module.System.Area.Localization;
using Tiger.Module.System.Localization;
using Tiger.Module.System.Platform.Localization;
using Tiger.Module.System.TextTemplate.Localization;
using Tiger.Module.TaskManagement.Localization;
using Tiger.Volo.Abp.Sass.Localization;
using Tiger.Volo.Abp.SettingUi.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.Localization;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.IdentityServer;
using Volo.Abp.IdentityServer.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.Localization;
using Volo.Abp.TenantManagement;
using Volo.Abp.UI;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Tiger
{
    /// <summary>
    /// 项目包含常量,枚举和其他对象,这些对象实际上是领域层的一部分,但是解决方案中所有的层/项目中都会使用到.
    /// 该项目不依赖解决方案中的其他项目. 其他项目直接或间接依赖该项目
    /// 例如 Book 实体和 IBookRepository 接口都适合放在这个项目中.
    /// </summary>
    [DependsOn(
        typeof(AbpUiModule),
        typeof(AbpAuditLoggingDomainSharedModule),
        typeof(AbpBackgroundJobsDomainSharedModule),
        typeof(AbpFeatureManagementDomainSharedModule),
        typeof(AbpIdentityDomainSharedModule),
        typeof(AbpIdentityServerDomainSharedModule),
        typeof(AbpPermissionManagementDomainSharedModule),
        typeof(AbpSettingManagementDomainSharedModule),
        typeof(AbpTenantManagementDomainSharedModule)
        )]
    public class TigerDomainSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            TigerGlobalFeatureConfigurator.Configure();
            TigerModuleExtensionConfigurator.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            #region Rescource 资源配置

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                // "TigerDomainSharedModule" 是项目的根命名空间名字. 如果你的项目的根命名空间名字为空,则无需传递此参数.
                options.FileSets.AddEmbedded<TigerDomainSharedModule>();
            });

            // 定义新的资源类 需要在模块中引入配置
            Configure<AbpLocalizationOptions>(options =>
            {
                // 扩展现有的资源 https://docs.abp.io/zh-Hans/abp/latest/Localization
                options.Resources
                    .Get<AbpUiResource>()
                    .AddVirtualJson("/Localization/AbpUi/Extensions");

                options.Resources
                        .Add<TigerResource>("zh-Hans")
                        .AddBaseTypes(typeof(AbpValidationResource))
                        .AddBaseTypes(typeof(AbpLocalizationResource))
                        .AddVirtualJson("/Localization/Tiger");// 用JSON文件存储本地化字符串. 使用虚拟文件系统 将JSON文件嵌入到程序集中.

                options.Resources
                       .Add<AbpSaasResource>("zh-Hans")
                       .AddBaseTypes(typeof(AbpUiResource))
                       .AddVirtualJson("/Volo/Abp/Sass/Localization/Resources");

                options.Resources
                       .Add<AbpOssManagementResource>("zh-Hans")
                       .AddVirtualJson("/Module/OssManagement/Localization/Resources");

                options.Resources
                       .Add<AbpAreaResources>("zh-Hans")
                       .AddVirtualJson("/Module/System/Area/Localization/Resources");

                options.Resources
                       .Add<PlatformResource>("zh-Hans")
                       .AddVirtualJson("/Module/System/Platform/Localization/Resources");

                options.Resources
                       .Add<AbpLocalizationResource>("zh-Hans")
                       .AddVirtualJson("/Module/System/Localization/Resources");

                options.Resources
                        .Get<IdentityResource>()  // 扩展现有资源(和abp框架默认的资源路径相同就会覆盖)
                        .AddVirtualJson("/Volo/Abp/Identity/Localization/Extensions");

                options.Resources
                        .Get<AbpIdentityServerResource>()
                        .AddVirtualJson("/Volo/Abp/IdentityServer/Localization/Resources")
                        .AddVirtualJson("/Volo/Abp/IdentityServer/Localization/SmsValidator/Resources");

                options.Resources
                        .Get<AuditLoggingResource>() // 扩展现有的资源
                        .AddVirtualJson("/Volo/Abp/AuditLogging/Localization/Extensions");

                options.Resources
                       .Add<SettingUiResource>("en")
                       .AddBaseTypes(typeof(AbpSettingManagementResource))
                       .AddVirtualJson("/Volo/Abp/SettingUi/Localization/SettingUi");

                options.Resources
                       .Add<AbpTextTemplateResource>("zh-Hans")
                       .AddVirtualJson("/Module/System/TextTemplate/Localization/Resources");

                options.Resources
                       .Add<TaskManagementResource>("zh-Hans")
                       .AddVirtualJson("/Module/TaskManagement/Localization/Resources");

                options.Resources
                       .Add<ExamResources>("zh-Hans")
                       .AddVirtualJson("/Module/Exams/Localization/Resources");

                options.Resources
                       .Add<SchoolResources>("zh-Hans")
                       .AddVirtualJson("/Module/Schools/Localization/Resources");

                options.Resources
                       .Add<QuestionBankResources>("zh-Hans")
                       .AddVirtualJson("/Module/QuestionBank/Localization/Resources");

                options.DefaultResourceType = typeof(TigerResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Tiger", typeof(TigerResource));
            }); 
            #endregion

            // The [Virtual File System](Virtual-File-System.md) requires to add your files in the `ConfigureServices` method of your [module](Module-Development-Basics.md) class:
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<TigerDomainSharedModule>("Tiger");
            });
        }
    }
}
