using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.System.Localization.Dtos;

[Serializable]
public class LanguageText : AuditedEntityDto<Guid>
{
    /// <summary>
    /// 资源名称
    /// </summary>
    public string ResourceName { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    public string Value { get; set; }
}