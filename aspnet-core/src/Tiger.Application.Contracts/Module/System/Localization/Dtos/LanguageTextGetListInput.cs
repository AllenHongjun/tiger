using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.System.Localization.Dtos;

[Serializable]
public class LanguageTextGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 语言
    /// </summary>
    public string CultureName { get; set; }

    /// <summary>
    /// 资源
    /// </summary>
    public string ResourceName { get; set; }

    /// <summary>
    /// 查询条件 name or value
    /// </summary>
    public string Filter { get; set; }
}