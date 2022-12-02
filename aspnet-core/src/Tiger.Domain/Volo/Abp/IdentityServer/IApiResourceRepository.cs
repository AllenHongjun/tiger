using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Volo.Abp.IdentityServer
{
    public interface IApiResourceRepository : Volo.Abp.IdentityServer.ApiResources.IApiResourceRepository
    {
        Task<List<string>> GetNamesAsync(CancellationToken cancellationToken = default);
    }
}
