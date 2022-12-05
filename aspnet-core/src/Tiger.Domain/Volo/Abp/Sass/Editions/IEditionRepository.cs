using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Volo.Abp.Sass.Editions
{   
    /// <summary>
    /// 版本仓储接口
    /// </summary>
    public interface IEditionRepository :IBasicRepository<Edition,Guid>
    {

        Task<bool> CheckUsedByTenantAsync(
            Guid tenantId, CancellationToken cancellationToken = default);

        Task<Edition> FindByDisplayNameAsync(string displayName, CancellationToken cancellationToken = default);

        Task<Edition> FindByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default);

        Task<Edition> GetListAsync(
            string sorting = null,
            int maxResultCount = 50,
            int skipCount = 0,
            string filter = null,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(string filter = null, 
            CancellationToken cancellationToken= default);
    }
}
