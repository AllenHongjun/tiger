using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.QuestionBank;

public class QuestionAnswerRepository : EfCoreRepository<TigerDbContext, QuestionAnswer, Guid>, IQuestionAnswerRepository
{
    public QuestionAnswerRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override  IQueryable<QuestionAnswer> WithDetails()
    {
        return GetQueryable().IncludeDetails();
    }
}