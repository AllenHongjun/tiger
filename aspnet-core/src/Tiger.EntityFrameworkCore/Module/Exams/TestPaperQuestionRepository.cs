using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.Exams;

public class TestPaperQuestionRepository : EfCoreRepository<TigerDbContext, TestPaperQuestion, Guid>, ITestPaperQuestionRepository
{
    public TestPaperQuestionRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override IQueryable<TestPaperQuestion> WithDetails()
    {
        return GetQueryable().IncludeDetails();
    }
}