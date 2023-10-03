using Tiger.Module.System.Cache.Localization;
using Tiger.Volo.Abp.Sass.Localization;
using Volo.Abp.Account;
using Volo.Abp.Account.Localization;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FluentValidation;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.VirtualFileSystem;

namespace Tiger
{
    /// <summary>
    /// 项目主要包含 应用服务 interfaces 和应用层的 数据传输对象 (DTO). 它用于分离应用层的接口和实现. 这种方式可以将接口项目做为约定包共享给客户端.
    /// 例如 IBookAppService 接口和 BookCreationDto 类都适合放在这个项目中.
    /// </summary>
    [DependsOn(
        typeof(TigerDomainSharedModule),
        typeof(AbpAccountApplicationContractsModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpTenantManagementApplicationContractsModule),
        typeof(AbpObjectExtendingModule),
        typeof(AbpFluentValidationModule) //Add the FluentValidation module
    )]
    public class TigerApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            TigerDtoExtensions.Configure();
        }

        

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            #region 本地化资源配置
            Configure<AbpVirtualFileSystemOptions>(options =>
               {
                   // "TigerApplicationContractsModule" 是项目的根命名空间名字. 如果你的项目的根命名空间名字为空,则无需传递此参数.
                   options.FileSets.AddEmbedded<TigerApplicationContractsModule>();
               });

            Configure<AbpLocalizationOptions>(options =>
            {
                // 扩展abp的account模块资源文件 注意虚拟路径需要和abp框架的区分开，不然会覆盖框架原有的资源文件
                options.Resources
                      .Get<AccountResource>()
                      .AddVirtualJson("/Volo/Abp/Account/Extensions/Localization/Resources");

                options.Resources
                      .Add<CacheResource>("zh-Hans")
                      .AddVirtualJson("/Module/System/Cache/Localization/Resources");
            }); 
            #endregion
        }
    }
}
