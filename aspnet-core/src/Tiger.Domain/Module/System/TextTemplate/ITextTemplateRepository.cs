using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.System.TextTemplate;

/// <summary>
/// 文本模板仓储接口
/// </summary>
public interface ITextTemplateRepository : IRepository<TextTemplate, Guid>
{   
    /// <summary>
    /// 根据名称查找文本模板
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="culture">文化</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TextTemplate> FindByNameAsync(string name, string culture = null, CancellationToken cancellationToken = default);
}
