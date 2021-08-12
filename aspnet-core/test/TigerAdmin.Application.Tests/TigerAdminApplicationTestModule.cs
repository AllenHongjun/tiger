using Volo.Abp.Modularity;

namespace Tiger
{
    [DependsOn(
        typeof(TigerApplicationModule),
        typeof(TigerDomainTestModule)
        )]
    public class TigerApplicationTestModule : AbpModule
    {

    }
}