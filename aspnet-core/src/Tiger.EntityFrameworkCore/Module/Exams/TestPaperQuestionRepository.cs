using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
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


    public async Task<List<TestPaperQuestion>> GetAllListAsync(Guid? testPaperSectionId,
            string sorting = null, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<TestPaperQuestion>().Include(x => x.Question)
                .WhereIf(testPaperSectionId.HasValue, x => x.TestPaperSectionId == testPaperSectionId)
                .OrderBy(sorting.IsNullOrEmpty() ? nameof(TestPaperQuestion.CreationTime) : sorting)
                .ToListAsync(GetCancellationToken(cancellationToken));

    }

    public override IQueryable<TestPaperQuestion> WithDetails()
    {
        return GetQueryable().IncludeDetails();
    }
}