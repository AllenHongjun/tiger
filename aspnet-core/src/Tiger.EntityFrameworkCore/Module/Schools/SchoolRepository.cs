using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.Schools;

public class SchoolRepository : EfCoreRepository<TigerDbContext, School, Guid>, ISchoolRepository
{
    public SchoolRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override IQueryable<School> WithDetails()
    {
        return GetQueryable().IncludeDetails(); // Uses the extension method defined above
    }
}