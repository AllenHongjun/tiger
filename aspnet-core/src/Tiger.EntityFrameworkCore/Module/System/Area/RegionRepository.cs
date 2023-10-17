using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.System.Area;

public class RegionRepository : EfCoreRepository<TigerDbContext, Region, int>, IRegionRepository
{
    public RegionRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    //public override async Task<IQueryable<Region>> WithDetailsAsync()
    //{
    //    return (await GetQueryableAsync()).IncludeDetails();
    //}
}