using Tiger.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tiger
{
    [DependsOn(
        typeof(TigerEntityFrameworkCoreTestModule)
        )]
    public class TigerDomainTestModule : AbpModule
    {

    }
}