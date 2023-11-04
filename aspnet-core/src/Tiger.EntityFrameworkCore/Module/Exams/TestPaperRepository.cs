using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.Exams;

public class TestPaperRepository : EfCoreRepository<TigerDbContext, TestPaper, Guid>, ITestPaperRepository
{
    public TestPaperRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override IQueryable<TestPaper> WithDetails()
    {
        return GetQueryable().IncludeDetails();
    }
}