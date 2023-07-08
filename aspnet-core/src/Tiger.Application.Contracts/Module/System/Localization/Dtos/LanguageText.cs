using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.System.Localization.Dtos;

[Serializable]
public class LanguageText : AuditedEntityDto<Guid>
{
    /// <summary>
    /// ��Դ����
    /// </summary>
    public string ResourceName { get; set; }

    /// <summary>
    /// ����
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// ֵ
    /// </summary>
    public string Value { get; set; }
}