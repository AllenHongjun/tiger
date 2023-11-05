using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.QuestionBank;

public class QuestionRepository : EfCoreRepository<TigerDbContext, Question, Guid>, IQuestionRepository
{
    public QuestionRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override  IQueryable<Question> WithDetails()
    {
        return  GetQueryable().IncludeDetails();
    }
}