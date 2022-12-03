using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiger.Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.IdentityServer.IdentityResources;

namespace Tiger.Volo.Abp.IdentityServer
{
    public class EfCoreIdentityResourceRepository : IdentityResourceRepository, ITigerIdentityResourceRepository
    {
        public EfCoreIdentityResourceRepository(
            IDbContextProvider<IIdentityServerDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

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
