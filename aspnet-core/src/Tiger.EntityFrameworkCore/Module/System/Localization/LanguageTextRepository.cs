using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.System.Localization;

/// <summary>
/// 语言文本
/// </summary>
public class LanguageTextRepository : EfCoreRepository<TigerDbContext, LanguageText, Guid>, ILanguageTextRepository
{
    public LanguageTextRepository(
        IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<LanguageText> FindAsync(string cultureName, string resourceName, string name)
    {
        return await DbSet
            .Where(x => x.CultrueName == cultureName)
            .Where(x => x.ResourceName == resourceName)
            .FirstOrDefaultAsync();
    }

    public async Task<long> GetCountAsync(string cultureName, string resourceName, string filter = null)
    {
        return await DbSet
            .WhereIf(cultureName.IsNullOrWhiteSpace(), e => e.CultrueName == cultureName)
            .WhereIf(resourceName.IsNullOrWhiteSpace(), e => e.ResourceName == resourceName)
            .WhereIf(filter.IsNullOrWhiteSpace(), e => e.Key.Contains(filter) || e.Value.Contains(filter))
            .CountAsync();
    }

    /// <summary>
    /// 分页获取语言文本列表
    /// </summary>
    /// <param name="cultureName"></param>
    /// <param name="resourceName"></param>
    /// <param name="filter"></param>
    /// <param name="maxResultCount"></param>
    /// <param name="skipCount"></param>
    /// <returns></returns>
    public async Task<List<LanguageText>> GetListAsync(string cultureName, string resourceName, string filter = null, int maxResultCount = 10, int skipCount = 0)
    {
        return await DbSet
            .WhereIf(cultureName.IsNullOrWhiteSpace(), e => e.CultrueName == cultureName)
            .WhereIf(resourceName.IsNullOrWhiteSpace(), e => e.ResourceName == resourceName)
            .WhereIf(filter.IsNullOrWhiteSpace(), e => e.Key.Contains(filter) || e.Value.Contains(filter))
            .OrderByDescending(e => e.CreationTime)
            .PageBy(skipCount,maxResultCount)
            .ToListAsync();
    }
    
}