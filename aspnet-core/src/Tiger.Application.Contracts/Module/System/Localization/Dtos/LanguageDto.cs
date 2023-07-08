using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.System.Localization.Dtos;

[Serializable]
public class LanguageTextDto : AuditedEntityDto<Guid>
{   
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enable { get; set; }

    /// <summary>
    /// 语言名称
    /// </summary>
    public string CultureName { get; set; }

    /// <summary>
    /// Ui语言名称
    /// </summary>
    public string UiCultureName { get; set; }

    /// <summary>
    /// 显示名称
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string FlagIcon { get; set; }

    /// <summary>
    /// 是否默认语言
    /// </summary>
    public bool IsDefault { get; set; }
}