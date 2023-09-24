using System;
using System.ComponentModel.DataAnnotations;

namespace Tiger.Module.System.Localization.Dtos;

[Serializable]
public class CreateLanguageDto
{
    ///// <summary>
    ///// ��Դ����
    ///// </summary>
    //[Required(ErrorMessage = "��Դ���Ʋ���Ϊ��")]
    //public string ResourceName { get; set; }
    /// <summary>
    /// ��������
    /// </summary>
    [Required(ErrorMessage = "�������Ʋ���Ϊ��")]
    public string CultureName { get; set; }

    /// <summary>
    /// �Ƿ�����
    /// </summary>
    public bool Enable { get; set; }


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