using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.Exams;

public class ExamRepository : EfCoreRepository<TigerDbContext, Exam, Guid>, IExamRepository
{
    public ExamRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override IQueryable<Exam> WithDetails()
    {
        return GetQueryable().IncludeDetails();
    }
}