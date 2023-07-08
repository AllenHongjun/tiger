using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.System.Localization.Dtos;

[Serializable]
public class LanguageTextGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// ����
    /// </summary>
    public string CultureName { get; set; }

    /// <summary>
    /// ��Դ
    /// </summary>
    public string ResourceName { get; set; }

    /// <summary>
    /// ��ѯ���� name or value
    /// </summary>
    public string Filter { get; set; }
}