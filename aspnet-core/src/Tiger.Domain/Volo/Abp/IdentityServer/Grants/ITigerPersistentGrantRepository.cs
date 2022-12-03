using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.IdentityServer.Grants;

namespace Volo.Abp.IdentityServer
{
    /// <summary>
    /// 持续授权
    /// </summary>
    public interface ITigerPersistentGrantRepository : IPersistentGrantRepository
    {

        Task<long> GetCountAsync(
            string subjectId = null,
            string filter = null,
            CancellationToken cancellation = default);

        Task<List<PersistedGrant>> GetListAsync(
            string subjectId = null,
            string filter = null,
            string sorting = nameof(PersistedGrant.CreationTime),
            int skipCount = 1,
            int maxResultCount = 10,
            CancellationToken cancellation = default);
    }
}
