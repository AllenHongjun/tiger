using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Tiger.Volo.Abp.Sass.Editions;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Tiger.Volo.Abp.Sass.Tenants;
// 限制是自己的实体类
using Tenant = Tiger.Volo.Abp.Sass.Tenants.Tenant;

namespace Tiger.Volo.Abp.Sass
{
    /// <summary>
    /// 版本仓储实现
    /// </summary>
    /// <remarks>
    /// 集成领域层，实现领域层的接口
    /// 也可以又 mongodb层来 实现领域层的接口
    /// </remarks>
    public class EditionRepository : EfCoreRepository<TigerDbContext, Edition, Guid>, IEditionRepository
    {
        public EditionRepository(
            IDbContextProvider<TigerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 判断版本是否关联租户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async virtual Task<bool> CheckUsedByTenantAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // 如何 需要扩充 tenant 的字段  如何覆盖abp自带的实体类？？ 

            return await DbContext.Set<Tenant>()
                .AnyAsync(x => x.EditionId == id, GetCancellationToken(cancellationToken));
            throw new NotImplementedException();
        }

        public async Task<Edition> FindByDisplayNameAsync(string displayName, CancellationToken cancellationToken = default)
        {
            return await (DbContext.Set<Edition>()
                    .OrderBy(t => t.Id)
                    .FirstOrDefaultAsync(t => t.DisplayName == displayName, GetCancellationToken(cancellationToken)));
        }

        /// <summary>
        /// 查询租户关联的版本
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Edition> FindByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {
            var editionDbSet = DbContext.Set<Edition>();
            var tenantDbSet = DbContext.Set<Tenant>();

            var queryable = from tenant in tenantDbSet
                            join edition in editionDbSet
                                on tenant.EditionId equals edition.Id
                            where tenant.Id == tenantId
                            select edition;
            return await queryable.OrderBy(t => t.Id)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 查询版本的数量
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<long> GetCountAsync(string filter = null, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<Edition>()
                    .WhereIf(!filter.IsNullOrEmpty(), x => x.DisplayName.Contains(filter))
                    .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 查询版本列表
        /// </summary>
        /// <param name="sorting"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="skipCount"></param>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Edition>> GetListAsync(string sorting = null, int maxResultCount = 50, int skipCount = 0, string filter = null, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<Edition>()
                    .WhereIf(!filter.IsNullOrEmpty(), x => x.DisplayName.Contains(filter))
                    .OrderBy(sorting.IsNullOrEmpty() ? nameof(Edition.DisplayName) : sorting)
                    .PageBy(skipCount, maxResultCount)
                    .ToListAsync(GetCancellationToken(cancellationToken));

        }
    }
}
