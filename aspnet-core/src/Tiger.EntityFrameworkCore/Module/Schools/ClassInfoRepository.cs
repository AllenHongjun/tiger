using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.Schools;

public class ClassInfoRepository : EfCoreRepository<TigerDbContext, ClassInfo, Guid>, IClassInfoRepository
{
    public ClassInfoRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override IQueryable<ClassInfo> WithDetails()
    {
        return  GetQueryable().IncludeDetails();
    }
}