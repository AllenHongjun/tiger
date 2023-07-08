using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.System.Localization;

/// <summary>
/// ���Թ���ִ��ӿ�
/// </summary>
public interface ILanguageRepository : IRepository<Language, Guid>
{
    Task<List<Language>> GetListAsync(bool? isEnabled =null);

    /// <summary>
    /// ��ѯ�����б�
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
    /// ��ȡ����
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<long> CountAsync(string filter = null);

    /// <summary>
    /// ��ѯָ������
    /// </summary>
    /// <param name="cultureName"></param>
    /// <returns></returns>
    Task<Language> FindAsync(string cultureName);

    /// <summary>
    /// ��ȡĬ������
    /// </summary>
    /// <returns></returns>
    Task<Language> FindDefaultLanguageAsync();
}
