using System;
using System.ComponentModel.DataAnnotations;

namespace Tiger.Module.System.Localization.Dtos;

[Serializable]
public class UpdateLanguageDto
{
    public bool Enable { get; set; }

    /// <summary>
    /// 语言名称
    /// </summary>
    [Required(ErrorMessage = "语言名称不能为空")]
    public string CultureName { get; set; }

    [Required(ErrorMessage = "Ui语言名称不能为空")]
    public string UiCultureName { get; set; }

    [Required(ErrorMessage = "显示名称不能为空")]
    public string DisplayName { get; set; }

    public string FlagIcon { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [Required(ErrorMessage = "是否启用不能为空")]
    public bool IsEnabled { get; set; }
}