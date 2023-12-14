using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.Exams;

public class TestPaperJudgeRepository : EfCoreRepository<TigerDbContext, TestPaperJudge, Guid>, ITestPaperJudgeRepository
{
    public TestPaperJudgeRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override IQueryable<TestPaperJudge> WithDetails()
    {
        return  GetQueryable().IncludeDetails();
    }
}