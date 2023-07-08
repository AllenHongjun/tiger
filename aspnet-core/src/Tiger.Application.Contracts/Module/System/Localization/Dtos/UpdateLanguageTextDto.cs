using System;
using System.ComponentModel.DataAnnotations;

namespace Tiger.Module.System.Localization.Dtos;

[Serializable]
public class UpdateLanguageTextDto
{
    /// <summary>
    /// ��Դ����
    /// </summary>
    [Required(ErrorMessage = "��Դ���Ʋ���Ϊ��")]
    public string ResourceName { get; set; }
    /// <summary>
    /// ��������
    /// </summary>
    [Required(ErrorMessage = "�������Ʋ���Ϊ��")]
    public string CultureName { get; set; }
    /// <summary>
    /// ����
    /// </summary>
    [Required(ErrorMessage = "���Ʋ���Ϊ��")]
    public string Name { get; set; }
    /// <summary>
    /// ֵ
    /// </summary>
    [Required(ErrorMessage = "ֵ����Ϊ��")]
    public string Value { get; set; }
}