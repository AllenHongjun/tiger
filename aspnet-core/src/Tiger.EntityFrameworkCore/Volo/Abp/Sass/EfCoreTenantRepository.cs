﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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


        public async override Task<Tenant> FindAsync(Guid id, bool includeDetails = true,
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
            bool includeDetails = false, 
            CancellationToken cancellationToken = default)
        {

            var editionDbSet = DbContext.Set<Edition>();
            var tenantDbSet = DbContext.Set<Tenant>().Include(x => x.Edition);

            var queryable = tenantDbSet
                .WhereIf(!filter.IsNullOrWhiteSpace(), u => u.Name.Contains(filter))
                .OrderBy<Tenant>(sorting.IsNullOrEmpty() ? nameof(Tenant.Name) : sorting);
            
            // linq join查询
            var combinedResult = await queryable
                .Join(editionDbSet,o => o.EditionId, i => i.Id,
                (tenant, edition) => new {tenant, edition})
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));

            // 没有使用导航属性，将版本的属性赋值到 tenant对象当中
            return combinedResult.Select(s =>
            {
                s.tenant.Edition = s.edition;
                return s.tenant;
            }).ToList();
        }
    }
}