using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.QuestionBank;

public class QuestionAttachmentRepository : EfCoreRepository<TigerDbContext, QuestionAttachment, Guid>, IQuestionAttachmentRepository
{
    public QuestionAttachmentRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override  IQueryable<QuestionAttachment> WithDetails()
    {
        return  GetQueryable().IncludeDetails();
    }
}