using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.System.TextTemplate;

/// <summary>
/// �ı�ģ��ִ��ӿ�
/// </summary>
public interface ITextTemplateRepository : IRepository<TextTemplate, Guid>
{   
    /// <summary>
    /// �������Ʋ����ı�ģ��
    /// </summary>
    /// <param name="name">����</param>
    /// <param name="culture">�Ļ�</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TextTemplate> FindByNameAsync(string name, string culture = null, CancellationToken cancellationToken = default);
}
