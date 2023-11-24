using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.Exams;

public class TestPaperSectionRepository : EfCoreRepository<TigerDbContext, TestPaperSection, Guid>, ITestPaperSectionRepository
{
    public TestPaperSectionRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override  IQueryable<TestPaperSection> WithDetails()
    {
        return  GetQueryable().IncludeDetails();
    }
}