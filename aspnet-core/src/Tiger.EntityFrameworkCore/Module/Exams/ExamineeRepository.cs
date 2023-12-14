using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.Exams;

public class ExamineeRepository : EfCoreRepository<TigerDbContext, Examinee, Guid>, IExamineeRepository
{
    public ExamineeRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override IQueryable<Examinee> WithDetails()
    {
        return  GetQueryable().IncludeDetails();
    }
}