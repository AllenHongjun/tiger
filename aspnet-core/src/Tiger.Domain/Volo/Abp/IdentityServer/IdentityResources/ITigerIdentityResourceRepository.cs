using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.IdentityServer.IdentityResources;

namespace Tiger.Volo.Abp.IdentityServer.IdentityResources
{
    public interface ITigerIdentityResourceRepository:IIdentityResourceRepository
    {
        Task<List<string>> GetNameAsync(CancellationToken cancellation = default);
    }
}
