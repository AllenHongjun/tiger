using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.System.Platform.Versions
{
    public interface IVersionRepository:IBasicRepository<AppVersion, Guid>
    {
        Task<bool> ExistsAsync(
            PlatformType platformType,
            string version,
            CancellationToken cancellationToken = default);

        Task<AppVersion> GetByVersionAsync(
            PlatformType platformType,
            string version,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            PlatformType platformType,
            string filter = "",
            CancellationToken cancellationToken = default);

        Task<AppVersion> GetPagedListAsync(
            PlatformType platformType,
            CancellationToken cancellationToken= default);
    }
}
