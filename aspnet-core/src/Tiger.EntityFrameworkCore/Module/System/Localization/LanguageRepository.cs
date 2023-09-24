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
/// ���Թ���ִ�ʵ��
/// </summary>
public class LanguageRepository : EfCoreRepository<TigerDbContext, Language, Guid>, ILanguageRepository
{
    public LanguageRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    

    /// <summary>
    /// ��ѯ��������
    /// </summary>
    /// <param name="cultureName">�Ļ�����</param>
    /// <returns></returns>
    public async Task<Language> FindAsync(string cultureName)
    {
        return await DbSet
            .Where(x => x.CultureName == cultureName)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// ��ѯĬ������
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
    ///  ��ҳ��ѯ�����б�����
    /// </summary>
    /// <param name="maxResultCount">ÿҳ����</param>
    /// <param name="skipCount"></param>
    /// <param name="filter">��ѯ����</param>
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
    /// ��ѯ����������
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