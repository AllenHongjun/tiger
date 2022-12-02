﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.IdentityServer;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.EntityFrameworkCore;

namespace Volo.Abp.IdentityServer
{
    [Dependency(ServiceLifetime.Transient)]
    [ExposeServices(
        typeof(IApiResourceRepository),
        typeof(ApiResourceRepository),
        typeof(Volo.Abp.IdentityServer.ApiResources.IApiResourceRepository))]
    public class EfCoreApiResourceRepository : ApiResourceRepository, IApiResourceRepository
    {
        public EfCoreApiResourceRepository(
            IDbContextProvider<IIdentityServerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async virtual Task<List<string>> GetNamesAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Select(x => x.Name)
                .Distinct()
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
