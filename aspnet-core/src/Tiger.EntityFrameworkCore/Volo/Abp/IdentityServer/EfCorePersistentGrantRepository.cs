using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.IdentityServer;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.IdentityServer.Grants;

namespace Tiger.Volo.Abp.IdentityServer
{
    // 依赖注入系统允许为一个接口注册多个服务. 注入接口时会解析最后一个注入的服务. 显式的替换服务是一个好习惯.
    //[Dependency(ReplaceServices = true)]
    // 手动公开服务接口

    /// <summary>
    /// 持久授予
    /// </summary>
    /// <remarks>
    /// 持久授权存储授权代码、刷新令牌和用户同意。
    /// </remarks>
    [ExposeServices(
        //typeof(Volo.Abp.IdentityServer.ITigerPersistentGrantRepository),
        typeof(IPersistentGrantRepository),
        typeof(PersistentGrantRepository))]
    public class EfCorePersistentGrantRepository : PersistentGrantRepository, ITigerPersistentGrantRepository
    {
        public EfCorePersistentGrantRepository(
            IDbContextProvider<IIdentityServerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="filter"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<long> GetCountAsync(string subjectId = null, string filter = null, CancellationToken cancellation = default)
        {
            var result =  await DbSet
                .WhereIf(!subjectId.IsNullOrWhiteSpace(), x => x.SubjectId.Equals(subjectId))
                .WhereIf(!filter.IsNullOrWhiteSpace(), x => 
                    x.Type.Contains(filter) || x.ClientId.Contains(filter) || x.Key.Contains(filter))
                .LongCountAsync(GetCancellationToken(cancellation));
            return result;
        }

        public async Task<List<PersistedGrant>> GetListAsync(string subjectId = null, string filter = null, string sorting = "CreationTime", int skipCount = 1, int maxResultCount = 10, CancellationToken cancellation = default)
        {   

            var result = await DbSet
                .WhereIf(!subjectId.IsNullOrWhiteSpace(), x => x.SubjectId.Equals(subjectId))
                .WhereIf(!filter.IsNullOrWhiteSpace(), x => 
                     x.Type.Contains(filter) || x.ClientId.Contains(filter) || x.Key.Contains(filter))
                // System.Linq.Dynamic.Core
                .OrderBy(sorting ?? nameof(PersistedGrant.CreationTime))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellation));
            return result;

        }
    }
}
