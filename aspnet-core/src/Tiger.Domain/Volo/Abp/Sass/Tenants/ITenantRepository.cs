using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Volo.Abp.Sass.Tenants
{   
    /// <summary>
    /// 租户接口
    /// </summary>
    public interface ITenantRepository: IBasicRepository<Tenant, Guid>
    {

        /// <summary>
        /// 通过名称查找租户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Tenant> FindByNameAsync(
            string name,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询租户列表
        /// </summary>
        /// <param name="sorting"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="skipCount"></param>
        /// <param name="filter"></param>
        /// <param name="editionId">版本Id</param>
        /// <param name="disableBeginTime">截止时间开始</param>
        /// <param name="disableEndTime">截止时间结束</param>
        /// <param name="isActive">是否启用</param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Tenant>> GetListAsync(
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null,
            Guid? editionId = null,
            DateTime? disableBeginTime = null,
            DateTime? disableEndTime = null,
            bool? isActive = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询租户数量
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(
            string filter = null,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 获取每个版本的租户数量
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<Guid?, int>> GetEditionTenantCount();

    }
}
