using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.Exams;

public class AnswerSheetRepository : EfCoreRepository<TigerDbContext, AnswerSheet, Guid>, IAnswerSheetRepository
{
    public AnswerSheetRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override IQueryable<AnswerSheet> WithDetails()
    {
        return GetQueryable().IncludeDetails();
    }
}