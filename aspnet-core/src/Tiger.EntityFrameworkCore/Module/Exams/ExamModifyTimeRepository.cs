using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.Exams;

public class ExamModifyTimeRepository : EfCoreRepository<TigerDbContext, ExamModifyTime, Guid>, IExamModifyTimeRepository
{
    public ExamModifyTimeRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override IQueryable<ExamModifyTime> WithDetails()
    {
        return  GetQueryable().IncludeDetails();
    }
}