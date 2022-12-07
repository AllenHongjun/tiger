using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiger.Volo.Abp.IdentityServer;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.EntityFrameworkCore;

namespace Volo.Abp.IdentityServer
{
    [Dependency(ServiceLifetime.Transient)]
    [ExposeServices(
        typeof(ITigerApiResourceRepository),
        typeof(IApiResourceRepository),
        typeof(ApiResourceRepository))]
    public class EfCoreApiResourceRepository : ApiResourceRepository, ITigerApiResourceRepository
    {
        public EfCoreApiResourceRepository(
            IDbContextProvider<IIdentityServerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async virtual Task<List<string>> GetNamesAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Select(x => x.Name)
                .Distinct()
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
