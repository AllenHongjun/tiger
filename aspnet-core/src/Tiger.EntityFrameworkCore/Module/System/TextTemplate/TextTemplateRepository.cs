using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.System.TextTemplate;

public class TextTemplateRepository : EfCoreRepository<TigerDbContext, TextTemplate, Guid>, ITextTemplateRepository
{
    public TextTemplateRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public Task<TextTemplate> FindByNameAsync(string name, string culture = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    //public override async Task<IQueryable<TextTemplate>> WithDetailsAsync()
    //{
    //    return (await GetQueryableAsync()).IncludeDetails();
    //}
}