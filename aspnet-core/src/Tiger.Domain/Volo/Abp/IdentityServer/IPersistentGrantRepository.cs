using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Volo.Abp.IdentityServer
{
    public interface IPersistentGrantRepository : Volo.Abp.IdentityServer.Grants.IPersistentGrantRepository
    {

        Task<long> GetCountAsync(
            string subjectId = null,
            string filter = null,
            CancellationToken cancellation = default);

        //Task<List<PersistedGrant>> GetListAsync(
        //    string subjectId = null,
        //    string filter)
    }
}
