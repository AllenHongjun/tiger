using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace TigerAdmin.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(TigerAdminHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class TigerAdminConsoleApiClientModule : AbpModule
    {
        
    }
}
