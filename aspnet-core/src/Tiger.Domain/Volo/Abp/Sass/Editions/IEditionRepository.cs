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

        /// <summary>
        /// 查询是否关联租户
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> CheckUsedByTenantAsync(
            Guid tenantId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据名称查询
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Edition> FindByDisplayNameAsync(string displayName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据租户id查询
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Edition> FindByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询版本列表
        /// </summary>
        /// <param name="sorting"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="skipCount"></param>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Edition>> GetListAsync(
            string sorting = null,
            int maxResultCount = 50,
            int skipCount = 0,
            string filter = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询版本数量
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> GetCountAsync(string filter = null, 
            CancellationToken cancellationToken= default);

       
    }
}
