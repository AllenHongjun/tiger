using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.System.Localization.Dtos;

[Serializable]
public class LanguageTextDto : AuditedEntityDto<Guid>
{   
    /// <summary>
    /// �Ƿ�����
    /// </summary>
    public bool Enable { get; set; }

    /// <summary>
    /// ��������
    /// </summary>
    public string CultureName { get; set; }

    /// <summary>
    /// Ui��������
    /// </summary>
    public string UiCultureName { get; set; }

    /// <summary>
    /// ��ʾ����
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// ͼ��
    /// </summary>
    public string FlagIcon { get; set; }

    /// <summary>
    /// �Ƿ�Ĭ������
    /// </summary>
    public bool IsDefault { get; set; }
}