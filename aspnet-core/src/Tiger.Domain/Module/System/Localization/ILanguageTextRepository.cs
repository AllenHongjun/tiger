using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.System.Localization;

/// <summary>
/// ”Ô—‘Œƒ±æ
/// </summary>
public interface ILanguageTextRepository : IRepository<LanguageText, Guid>
{
    Task<List<LanguageText>> GetListAsync(
        string cultureName,
        string resourceName,
        string filter = null,
        int maxResultCount = 10,
        int skipCount = 0);

    Task<long> GetCountAsync(string cultureName, string resourceName, string filter = null);

    Task<LanguageText> FindAsync(string cultureName, string resourceName, string name);
}
