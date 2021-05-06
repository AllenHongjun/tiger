using TigerAdmin.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace TigerAdmin
{
    [DependsOn(
        typeof(TigerAdminEntityFrameworkCoreTestModule)
        )]
    public class TigerAdminDomainTestModule : AbpModule
    {

    }
}