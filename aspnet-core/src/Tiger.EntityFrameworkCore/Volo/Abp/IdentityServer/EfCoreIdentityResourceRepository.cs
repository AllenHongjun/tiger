using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiger.Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.IdentityServer.IdentityResources;

namespace Tiger.Volo.Abp.IdentityServer
{

    //[Dependency(ServiceLifetime.Transient, LoadHint.Always)]
    public class EfCoreIdentityResourceRepository : IdentityResourceRepository, ITigerIdentityResourceRepository
    {
        public EfCoreIdentityResourceRepository(
            IDbContextProvider<IIdentityServerDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取所有的资源名称
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<string>> GetNameAsync(CancellationToken cancellation = default)
        {   
            var result = await DbSet
                         .Select(x => x.Name)
                         .Distinct()
                         .ToListAsync(GetCancellationToken(cancellation));

            throw new NotImplementedException();
        }
    }
}
