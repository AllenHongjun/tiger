using Volo.Abp.Modularity;

namespace TigerAdmin
{
    [DependsOn(
        typeof(TigerAdminApplicationModule),
        typeof(TigerAdminDomainTestModule)
        )]
    public class TigerAdminApplicationTestModule : AbpModule
    {

    }
}