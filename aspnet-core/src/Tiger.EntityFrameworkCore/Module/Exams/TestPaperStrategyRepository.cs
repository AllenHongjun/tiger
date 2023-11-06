using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.Exams;

public class TestPaperStrategyRepository : EfCoreRepository<TigerDbContext, TestPaperStrategy, Guid>, ITestPaperStrategyRepository
{
    public TestPaperStrategyRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override IQueryable<TestPaperStrategy> WithDetails()
    {
        return GetQueryable().IncludeDetails();
    }
}