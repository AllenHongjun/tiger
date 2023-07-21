using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.System.Platform.Versions
{
    public interface IVersionRepository : IBasicRepository<AppVersion, Guid>
    {
        /// <summary>
        /// 查询版本是否存在
        /// </summary>
        /// <param name="platformType"></param>
        /// <param name="version"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(
            PlatformType platformType,
            string version,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据版本名称获取版本详情
        /// </summary>
        /// <param name="platformType"></param>
        /// <param name="version"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AppVersion> GetByVersionAsync(
            PlatformType platformType,
            string version,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 查询版本数量
        /// </summary>
        /// <param name="platformType"></param>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> GetCountAsync(
            PlatformType platformType,
            string filter = "",
            CancellationToken cancellationToken = default);

        /// <summary>
        ///  分页查询版本列表
        /// </summary>
        /// <param name="platformType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>     
        public Task<List<AppVersion>> GetPagedListAsync(
            PlatformType platformType, string filter = "", string sorting = nameof(AppVersion.CreationTime),
            bool includeDetails = true, int skipCount = 1, int maxResultCount = 10,
            CancellationToken cancellationToken = default);
    }
}
