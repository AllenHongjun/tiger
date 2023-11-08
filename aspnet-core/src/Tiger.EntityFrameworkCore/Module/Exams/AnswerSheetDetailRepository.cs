using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.Exams;

public class AnswerSheetDetailRepository : EfCoreRepository<TigerDbContext, AnswerSheetDetail, Guid>, IAnswerSheetDetailRepository
{
    public AnswerSheetDetailRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override  IQueryable<AnswerSheetDetail> WithDetails()
    {
        return GetQueryable().IncludeDetails();
    }
}