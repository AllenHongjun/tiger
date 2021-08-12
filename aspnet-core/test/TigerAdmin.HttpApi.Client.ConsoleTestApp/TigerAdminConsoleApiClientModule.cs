using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Tiger.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(TigerHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class TigerConsoleApiClientModule : AbpModule
    {
        
    }
}
