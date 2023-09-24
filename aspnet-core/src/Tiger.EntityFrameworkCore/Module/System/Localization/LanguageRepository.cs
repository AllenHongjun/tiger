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
/// 语言管理仓储实现
/// </summary>
public class LanguageRepository : EfCoreRepository<TigerDbContext, Language, Guid>, ILanguageRepository
{
    public LanguageRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    

    /// <summary>
    /// 查询语言详情
    /// </summary>
    /// <param name="cultureName">文化名称</param>
    /// <returns></returns>
    public async Task<Language> FindAsync(string cultureName)
    {
        return await DbSet
            .Where(x => x.CultureName == cultureName)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// 查询默认语言
    /// </summary>
    /// <returns></returns>
    public async Task<Language> FindDefaultLanguageAsync()
    {
        return await DbSet
            .Where(x => x.IsDefault)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Language>> GetListAsync(bool? isEnabled = null)
    {
        var list = await DbSet
            .WhereIf(isEnabled != null, x => x.IsEnabled == isEnabled)
            .ToListAsync();
        return list;
    }

    /// <summary>
    ///  分页查询语言列表数据
    /// </summary>
    /// <param name="maxResultCount">每页数量</param>
    /// <param name="skipCount"></param>
    /// <param name="filter">查询条件</param>
    /// <returns></returns>
    public async Task<List<Language>> GetListAsync(int maxResultCount = 10, int skipCount = 0, string filter = null)
    {
        return await DbSet
            .WhereIf(!filter.IsNullOrWhiteSpace(), e => e.CultureName.Contains(filter) ||
            e.UiCultureName.Contains(filter) || e.DisplayName.Contains(filter))
            .OrderByDescending(e => e.CreationTime)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }


    /// <summary>
    /// 查询语言总数量
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<long> CountAsync(string filter = null)
    {
        return await DbSet
            .WhereIf(!filter.IsNullOrWhiteSpace(), e => e.CultureName.Contains(filter) ||
            e.UiCultureName.Contains(filter) || e.DisplayName.Contains(filter))
            .CountAsync();
    }
}