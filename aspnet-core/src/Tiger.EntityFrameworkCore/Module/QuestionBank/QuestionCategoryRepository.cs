using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.QuestionBank;

public class QuestionCategoryRepository : EfCoreRepository<TigerDbContext, QuestionCategory, Guid>, IQuestionCategoryRepository
{
    public QuestionCategoryRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
    

    public override  IQueryable<QuestionCategory> WithDetails()
    {
        return GetQueryable().IncludeDetails();
    }
}