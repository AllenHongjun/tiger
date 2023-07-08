using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.System.Localization;

/// <summary>
/// 语言管理仓储接口
/// </summary>
public interface ILanguageRepository : IRepository<Language, Guid>
{
    Task<List<Language>> GetListAsync(bool? isEnabled =null);

    /// <summary>
    /// 查询语言列表
    /// </summary>
    /// <param name="maxResultCount"></param>
    /// <param name="skipCount"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<List<Language>> GetListAsync(
        int maxResultCount = 10,
        int skipCount = 0,
        string filter = null);

    /// <summary>
    /// 获取数量
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<long> CountAsync(string filter = null);

    /// <summary>
    /// 查询指定语言
    /// </summary>
    /// <param name="cultureName"></param>
    /// <returns></returns>
    Task<Language> FindAsync(string cultureName);

    /// <summary>
    /// 获取默认语言
    /// </summary>
    /// <returns></returns>
    Task<Language> FindDefaultLanguageAsync();
}
