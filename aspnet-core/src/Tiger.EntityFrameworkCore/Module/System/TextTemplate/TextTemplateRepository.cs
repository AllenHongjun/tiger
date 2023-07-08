using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.System.TextTemplate;

/// <summary>
/// 文本模板
/// </summary>
public class TextTemplateRepository : EfCoreRepository<TigerDbContext, TextTemplate, Guid>, ITextTemplateRepository
{
    public TextTemplateRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    /// <summary>
    /// 根据名称查询文本模板
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="culture">文化名称</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TextTemplate> FindByNameAsync(string name, string culture = null, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .WhereIf(!name.IsNullOrWhiteSpace(), x => x.Name.Contains(name))
            .WhereIf(!culture.IsNullOrWhiteSpace(), x => x.Culture.Contains(culture))
            .FirstOrDefaultAsync(cancellationToken);
    }
    
}