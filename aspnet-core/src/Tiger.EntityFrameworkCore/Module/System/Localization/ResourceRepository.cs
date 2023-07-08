using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.System.Localization;

public class ResourceRepository : EfCoreRepository<TigerDbContext, Resource, Guid>, IResourceRepository
{
    public ResourceRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    //public override async Task<IQueryable<Resource>> WithDetailsAsync()
    //{
    //    return (await GetQueryableAsync()).IncludeDetails();
    //}
}