using System;
using System.ComponentModel.DataAnnotations;

namespace Tiger.Module.System.Localization.Dtos;

[Serializable]
public class UpdateLanguageDto
{
    public bool Enable { get; set; }

    /// <summary>
    /// ��������
    /// </summary>
    [Required(ErrorMessage = "�������Ʋ���Ϊ��")]
    public string CultureName { get; set; }

    [Required(ErrorMessage = "Ui�������Ʋ���Ϊ��")]
    public string UiCultureName { get; set; }

    [Required(ErrorMessage = "��ʾ���Ʋ���Ϊ��")]
    public string DisplayName { get; set; }

    public string FlagIcon { get; set; }

    /// <summary>
    /// �Ƿ�����
    /// </summary>
    [Required(ErrorMessage = "�Ƿ����ò���Ϊ��")]
    public bool IsEnabled { get; set; }
}