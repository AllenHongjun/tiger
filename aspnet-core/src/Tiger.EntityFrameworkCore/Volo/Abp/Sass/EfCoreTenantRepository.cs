using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tiger.EntityFrameworkCore;
using Tiger.Volo.Abp.Sass.Editions;
using Tiger.Volo.Abp.Sass.Tenants;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Volo.Abp.Sass
{
    /// <summary>
    /// 租户 仓储实现
    /// </summary>
    /// <remarks>
    /// Ef core 迁移文档里面 有如何
    /// </remarks>
    public class EfCoreTenantRepository : EfCoreRepository<TigerDbContext, Tenant, Guid>, ITenantRepository
    {
        public EfCoreTenantRepository(
            IDbContextProvider<TigerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取租户包含明细
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <remarks>
        /// 1. 重写了父类的方法，不然无法获取detail里面的值 原因暂不明
        /// </remarks>
        public override async Task<Tenant> GetAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<Tenant>().IncludeDetails(true).FirstOrDefaultAsync(x => x.Id == id);
        }




        public async  Task<Tenant> Find1Async(Guid id, bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            var tenantDbSet = DbContext.Set<Tenant>();

            if (includeDetails)
            {
                var editionDbSet = DbContext.Set<Edition>();
                var queryable = from tenant in tenantDbSet
                                join edition in editionDbSet on tenant.EditionId equals edition.Id into eg
                                // 左联查询
                                from e in eg.DefaultIfEmpty()
                                where tenant.Id.Equals(id)
                                orderby tenant.Id
                                select new
                                {
                                    Tenant = tenant,
                                    Edition = e,
                                };
                var result = await queryable
                    .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));

                if (result != null && result.Tenant != null)
                {
                    result.Tenant.Edition = result.Edition;
                }

                return result?.Tenant;
            }

            return await tenantDbSet
                .OrderBy(t => t.Id)
                .FirstOrDefaultAsync(t => t.Id == id, GetCancellationToken(cancellationToken));
        }

        public async virtual Task<Tenant> FindByNameAsync(string name, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            var dbContext =  DbContext;
            var tenantDbSet = dbContext.Set<Tenant>();

            if (includeDetails)
            {
                var editionDbSet = dbContext.Set<Edition>();
                var queryable = from tenant in tenantDbSet
                                join edition in editionDbSet on tenant.EditionId equals edition.Id into eg
                                from e in eg.DefaultIfEmpty()
                                where tenant.Name.Equals(name)
                                orderby tenant.Id
                                select new
                                {
                                    Tenant = tenant,
                                    Edition = e,
                                };
                var result = await queryable
                    .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
                if (result != null && result.Tenant != null)
                {
                    result.Tenant.Edition = result.Edition;
                }

                return result?.Tenant;
            }

            return await tenantDbSet
                .OrderBy(t => t.Id)
                .FirstOrDefaultAsync(t => t.Name == name, GetCancellationToken(cancellationToken));
        }

        [Obsolete("Use FindByNameAsync method.")]
        public virtual Tenant FindByName(string name, bool includeDetails = true)
        {
            var tenantDbSet = DbContext.Set<Tenant>()
                    .IncludeDetails(includeDetails);

            if (includeDetails)
            {
                var editionDbSet = DbContext.Set<Edition>();
                var queryable = from tenant in tenantDbSet
                                join edition in editionDbSet on tenant.EditionId equals edition.Id into eg
                                from e in eg.DefaultIfEmpty()
                                where tenant.Name.Equals(name)
                                orderby tenant.Id
                                select new
                                {
                                    Tenant = tenant,
                                    Edition = e,
                                };
                var result = queryable
                    .FirstOrDefault();
                if (result != null && result.Tenant != null)
                {
                    result.Tenant.Edition = result.Edition;
                }

                return result?.Tenant;
            }

            return tenantDbSet
                .OrderBy(t => t.Id)
                .FirstOrDefault(t => t.Name == name);
        }

        [Obsolete("Use FindAsync method.")]
        public virtual Tenant FindById(Guid id, bool includeDetails = true)
        {
            var tenantDbSet = DbContext.Set<Tenant>()
                    .IncludeDetails(includeDetails);

            if (includeDetails)
            {
                var editionDbSet = DbContext.Set<Edition>();
                var queryable = from tenant in tenantDbSet
                                join edition in editionDbSet on tenant.EditionId equals edition.Id into eg
                                from e in eg.DefaultIfEmpty()
                                where tenant.Id.Equals(id)
                                orderby tenant.Id
                                select new
                                {
                                    Tenant = tenant,
                                    Edition = e,
                                };
                var result = queryable
                    .FirstOrDefault();
                if (result != null && result.Tenant != null)
                {
                    result.Tenant.Edition = result.Edition;
                }

                return result?.Tenant;
            }

            return tenantDbSet
                .OrderBy(t => t.Id)
                .FirstOrDefault(t => t.Id == id);
        }

        public  async Task<int> GetCountAsync(string filter = null, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<Tenant>().Include(x => x.Edition)
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                u =>
                    u.Name.Contains(filter)
            ).CountAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<Tenant>> GetListAsync(
            string sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            string filter = null, 
            Guid? editionId = null,
            DateTime? disableBeginTime = null,
            DateTime? disableEndTime = null,
            bool? isActive = null,
            bool includeDetails = false, 
            CancellationToken cancellationToken = default)
        {

            var editionDbSet = DbContext.Set<Edition>();
            var tenantDbSet = DbContext.Set<Tenant>().Include(x => x.Edition);

            var queryable = tenantDbSet
                .WhereIf(!filter.IsNullOrWhiteSpace(), u => u.Name.Contains(filter))
                .WhereIf(disableBeginTime != null, u => u.DisableTime >= disableBeginTime)
                .WhereIf(disableEndTime != null, u => u.DisableTime <= disableEndTime)
                .WhereIf(isActive != null, u => u.IsActive == isActive)
                .OrderBy<Tenant>(sorting.IsNullOrEmpty() ? nameof(Tenant.Name) : sorting);


            // 修改为使用 left join查询
            var combinedResult = await (from tenant in queryable
                                 join edition in editionDbSet on tenant.EditionId equals edition.Id into eg
                                 from e in eg.DefaultIfEmpty()
                                 select new
                                 {
                                     Tenant = tenant,
                                     Edition = e,
                                 })
                                 .WhereIf(editionId != null, e => e.Edition.Id == editionId)
                                .Skip(skipCount)
                                .Take(maxResultCount)
                                .ToListAsync();
            
            return combinedResult.Select(s =>
            {
                s.Tenant.Edition = s.Edition;
                return s.Tenant;
            }).ToList();
        }



        /// <summary>
        /// 获取每个版本的租户数量
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<Guid?, int>> GetEditionTenantCount()
        {
            var list = await DbContext.Set<Tenant>()
               .Where(x => x.EditionId != null)
               .ToListAsync();
            var editionTenantCountDic = list.GroupBy(x => x.EditionId).ToDictionary(g => g.Key, g => g.Count());
            return editionTenantCountDic;
        }

        /// <summary>
        /// 包含子集
        /// </summary>
        /// <remarks>
        /// 重写父类方法，不然会获取不到
        /// </remarks>
        /// <returns></returns>
        public override IQueryable<Tenant> WithDetails()
        {
            return GetQueryable().IncludeDetails(); // Uses the extension method defined above
        }
    }
}
